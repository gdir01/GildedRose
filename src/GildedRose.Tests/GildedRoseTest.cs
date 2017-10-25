using System.Collections.Generic;
using GildedRose.Console;
using Xunit;

namespace GildedRose.Tests
{
    public class GildedRoseTest
    {
        [Fact]
        public void QualityInRange()
        {
            IList<Item> items = new List<Item>
                                    {
                                        new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 0},
                                        new Item {Name = "Aged Brie", SellIn = 2, Quality = 50},
                                    };
            UpdateStrategy.UpdateQuality(items);
            Assert.InRange(items[0].Quality, 0, 50);
            Assert.InRange(items[1].Quality, 0, 50);
        }
        [Fact]
        public void SulfuraConstant()
        {
            IList<Item> items = new List<Item>
                                    {
                                        new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                    };
            UpdateStrategy.UpdateQuality(items);
            Assert.Equal(80,items[0].Quality);
            Assert.Equal(0,items[0].SellIn);
        }
        [Fact]
        public void ConjuredTwiceDegrade()
        {
            IList<Item> items = new List<Item>
                                    {
                                        new Item {Name = "Conjured Mana Cake", SellIn = 1, Quality = 6}
                                    };
            UpdateStrategy.UpdateQuality(items);
            Assert.Equal(4, items[0].Quality);
            Assert.Equal(0, items[0].SellIn);
            UpdateStrategy.UpdateQuality(items);
            Assert.Equal(0, items[0].Quality);
            Assert.Equal(-1, items[0].SellIn);
        }
        [Fact]
        public void BackStagePassesReset()
        {
            IList<Item> items = new List<Item>
                                    {
                                        new Item
                                            {
                                                Name = "Backstage passes to a TAFKAL80ETC concert",
                                                SellIn = 0,
                                                Quality = 45
                                            },
                                    };
            UpdateStrategy.UpdateQuality(items);
            Assert.Equal(0, items[0].Quality);
        }
    }
}