using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Reflection;

namespace Webapi.AutoMapper
{
    public class Configuration
    {
        public static void Configure()
        {
            var assembly = Assembly.Load("Webapi.AutoMapper");
            var profiles = assembly.GetTypes().Where(t => t.Name.EndsWith("Profile")).ToList();
            Mapper.Initialize(cfg =>
            {
                profiles.ForEach(profile =>
                {
                    cfg.AddProfile(profile);
                });
            });
        }
    }
}
