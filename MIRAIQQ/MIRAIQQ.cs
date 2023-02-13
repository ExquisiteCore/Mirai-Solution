using Mirai.Net.Data.Events.Concretes.Group;
using Mirai.Net.Data.Events.Concretes.Message;
using Mirai.Net.Data.Messages.Concretes;
using Mirai.Net.Data.Messages.Receivers;
using Mirai.Net.Sessions;
using Mirai.Net.Sessions.Http.Managers;
using Mirai.Net.Utils.Scaffolds;
using System.Reactive.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Channels;
using Flurl.Http.Testing;
using Mirai.Net.Data.Events.Concretes.Request;
using Mirai.Net.Data.Shared;
using Newtonsoft.Json;

namespace MIRAIQQ
{
    
    class botqq
    {
        public static MiraiBot bot = new MiraiBot 
        { 
            Address = "localhost:8080",
            QQ = "3389583001",
            VerifyKey = "123456789"
        };
    }

    internal class MIRAIqq
    {
        static async Task Main(string[] args)
        {
            await botqq.bot.LaunchAsync();
            Console.WriteLine("开启成功!");
            botqq.bot.MessageReceived
                .OfType<FriendMessageReceiver>()
                .Subscribe(x =>
                {
                    string plain = x.MessageChain.GetPlainMessage();
                    if (plain.Contains("查询玩家"))
                    {
                        string ip = plain.Substring("查询玩家".Length); //("查询玩家");
                        if (ip == String.Empty)
                        {
                            x.SendMessageAsync("使用方式：查询服务器状态[ip]");
                            return;
                        }
                        else
                        {
                            var image = new ImageMessage
                            {
                                Url = $"https://motdbe.blackbe.work/status_img/java?host={ip}"
                            };
                            x.SendMessageAsync(image);
                            return;
                        }
                    }
                });
            
            botqq.bot.EventReceived
                 .OfType<NewMemberRequestedEvent>()
                 .Subscribe(x =>
                 {
                     if (x.GroupId == "758522310")
                     {
                         x.ApproveAsync();
                         Console.WriteLine($"{x.FromId}{x.Nick}加群了");
                         botqq.bot.MessageReceived
                             .OfType<GroupMessageReceiver>()
                             .Subscribe(a =>
                             {
                                 if (a.Sender.Id == x.FromId)
                                 {
                                     string numb;
                                     a.SendMessageAsync("请问1+1=?");
                                     a.SendMessageAsync("请谨慎答错就踢了你");
                                     numb = a.MessageChain.GetPlainMessage();
                                     Console.WriteLine(numb);
                                     if (numb == "2")
                                     {
                                         a.SendMessageAsync($"{x.Nick}欢迎你的加入");
                                         return;
                                     }
                                     else
                                     {
                                         a.SendMessageAsync($"{x.Nick}你可以走了");
                                         GroupManager.KickAsync(x.FromId,x.GroupId);
                                         return;
                                     }
                                     return;
                                 }
                             });
                     }
                 });

            while (true)
            {
                if (Console.ReadLine() == "stop")
                {
                    return;
                }
            }
        }
    }
}