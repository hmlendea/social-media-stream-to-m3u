using NuciCLI;

namespace StreamToM3U.Configuration
{
    public sealed class Options
    {
        static string[] InputFileOptions = { "-i", "--input" };
        static string[] OutputFileOptions = { "-o", "--output" };
        
        static string[] ChannelIdOptions = { "-c", "--channel" };
        static string[] TitleOptions = { "-t", "--title" };
        static string[] UrlOptions = { "-u", "--url" };

        static string[] YouTubeProcessorOptions = { "--yt", "--youtube" };
        static string[] TwitchProcessorOptions = { "--twitch" };
        static string[] SeeNowProcessorOptions = { "--seenow" };
        static string[] TvSportHdProcessorOptions = { "--tvs", "--tvsport", "--tvshd", "--tvsporthd" };
        static string[] AntenaPlayProccessorOptions = { "--antena-play", "--antenaplay", "--antena", "--aplay", "--ap" };

        public StreamProvider Provider { get; set; }

        public string InputFile { get; set; }
        public string OutputFile { get; set; }

        public string ChannelId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public static Options FromArguments(string[] args)
        {
            Options options = new Options();
            options.Provider = DetermineProviderFromArgs(args);
            options.InputFile = GetArgumentIfExists(args, InputFileOptions);
            options.OutputFile = GetArgumentIfExists(args, OutputFileOptions, "playlist.m3u");
            options.Title = GetArgumentIfExists(args, TitleOptions);
            options.Url = GetArgumentIfExists(args, UrlOptions);

            return options;
        }

        static StreamProvider DetermineProviderFromArgs(string[] args)
        {
            if (CliArgumentsReader.HasOption(args, YouTubeProcessorOptions))
            {
                return StreamProvider.YouTube;
            }

            if (CliArgumentsReader.HasOption(args, TwitchProcessorOptions))
            {
                return StreamProvider.Twitch;
            }

            if (CliArgumentsReader.HasOption(args, SeeNowProcessorOptions))
            {
                return StreamProvider.SeeNow;
            }

            if (CliArgumentsReader.HasOption(args, TvSportHdProcessorOptions))
            {
                return StreamProvider.TvSportHd;
            }

            if (CliArgumentsReader.HasOption(args, AntenaPlayProccessorOptions))
            {
                return StreamProvider.AntenaPlay;
            }
            
            return StreamProvider.Other;
        }

        static string GetArgumentIfExists(string[] args, string[] argumentOptions)
            => GetArgumentIfExists(args, argumentOptions, null);

        static string GetArgumentIfExists(string[] args, string[] argumentOptions, string fallbackValue)
        {
            if (CliArgumentsReader.HasOption(args, argumentOptions))
            {
                return CliArgumentsReader.GetOptionValue(args, argumentOptions);
            }

            return fallbackValue;
        }
    }
}
