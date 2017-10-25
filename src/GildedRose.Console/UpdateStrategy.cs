using System;
using System.Collections.Generic;

namespace GildedRose.Console
{
    public class UpdateStrategy
    {
       //Dictionary for selecting method to run. If new rules appear this has to be extended. And according method has to be added below.
       private static Dictionary<string, Func<Item,string >> dictStrategy = new Dictionary<string, Func<Item, string>>()
        {
            {"Sulfuras, Hand of Ragnaros", UpdateLegendary },
            {"Aged Brie", UpdateAgedBrie },
            {"Backstage passes to a TAFKAL80ETC concert", UpdateBackstagePasses },
            {"Conjured Mana Cake", UpdateConjured },
        };

        public static void UpdateQuality(IList<Item> items)
        {
            for (var i = 0; i < items.Count; i++)
            {
                if (dictStrategy.ContainsKey(items[i].Name))
                {
                    dictStrategy[items[i].Name](items[i]);
                }
                else
                {
                    UpdateRegular(items[i]);
                }
            }
        }

        static string UpdateLegendary(Item item)
        {
            return "ok";
        }

        // Particular method for every entry in dictionary dictStrategy:

        static string UpdateConjured(Item item)
        {
            item.SellIn--;
            int quality = item.Quality;
            quality--;
            quality--;
            if (item.SellIn<0)
            {
                quality--;
                quality--;
            }
            item.Quality = quality<0 ? 0:quality;
            return "ok";
        }
        
        static string UpdateAgedBrie(Item item)
        {
            item.SellIn--;
            if (item.Quality<50)
            {
                item.Quality++;
            }
            return "ok";
        }
        
        static string UpdateBackstagePasses(Item item)
        {
            item.SellIn--;
            if (item.SellIn<0)
            {
                item.Quality = 0;
            }
            else
            {
                int quality = item.Quality;
                if (quality<50)
                {
                    quality++;
                    if (item.SellIn<=10)
                    {
                        quality++;
                    }
                    if (item.SellIn<=5)
                    {
                        quality++;
                    }
                    item.Quality = quality>50 ? 50 : quality;
                }
            }
            return "ok";
        }
        
        static string UpdateRegular(Item item)
        {
            item.SellIn--;
            if (item.Quality > 0)
            {
                int quality = item.Quality;
                quality--;
                if (item.SellIn < 0)
                {
                    quality--;
                }
                item.Quality = quality < 0 ? 0 : quality;
            }
            return "ok";
        }
    }
}
