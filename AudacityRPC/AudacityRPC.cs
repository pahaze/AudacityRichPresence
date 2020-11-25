using System;
using System.Linq;
using System.Diagnostics;
using DiscordRPC;
using System.Threading;
using System.ComponentModel;

namespace AudacityRPC
{
    class AudacityRPC
    {
        static void Main(string[] args)
        {
            DiscordRpcClient DiscordClient = new DiscordRpcClient("769607179841568800");
            DiscordClient.Initialize();
            void SetPresence(RichPresence Presence)
            {
                DiscordClient.SetPresence(Presence);
            }
            for (; ; )
            {
                Thread.Sleep(500);
                var timesta = Timestamps.Now;
                for (; ; )
                {
                    Thread.Sleep(500);
                    Process process = Process.GetProcessesByName("audacity").LastOrDefault();
                    string audacityWindowName = "";
                    if (process != null)
                    {
                        audacityWindowName = process.MainWindowTitle;
                    }
                    else if(process == null)
                    {
                        audacityWindowName = "notrunning";
                    }
                    else
                    {
                        audacityWindowName = "";
                    }
                    string project = "";
                    string state = "";

                    if (String.IsNullOrEmpty(audacityWindowName))
                    {
                        project = "User is in menus...";
                        state = "Waiting...";
                    }
                    else if (audacityWindowName == "notrunning")
                    {
                        project = "Audacity is not open...";
                        state = "Waiting...";
                    }
                    else if (audacityWindowName == "Audacity")
                    {
                        project = "Working on an untitled project";
                        state = "Editing audio";
                    }
                    else
                    {
                        project = "Working on " + audacityWindowName;
                        state = "Editing audio";
                    }

                    SetPresence(new RichPresence()
                    {
                        Details = project,
                        State = state,
                        Timestamps = timesta,
                        Assets = new Assets()
                        {
                            LargeImageKey = "audacity",
                            LargeImageText = "Audacity",
                            SmallImageText = "",
                            SmallImageKey = ""
                        }
                    });
                }
            }
        }
    }
}
