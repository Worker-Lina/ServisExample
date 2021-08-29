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
                context.Files.AddRange(
                    new File
                    {
                        Name = "text.txt",
                        MineType = "txt",
                        Size = "13",
                        DateAdd = DateTime.Now
                    },
                    new File
                    {
                        Name = "Animal_sean_crane_61.jpg",
                        MineType = "jpg",
                        Size = "101",
                        DateAdd = DateTime.Now
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
