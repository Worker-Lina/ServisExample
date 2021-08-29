using ServiceExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceExample
{
    public class ContentDb
    {
        public static void Initialize(FileContext context)
        {
            if (!context.Files.Any())
            {
                context.Files.Add(
                    new File
                    {
                        Name = "text.txt",
                        MineType = "txt",
                        Size = "13 byte",
                        DateAdd = DateTime.Now
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
