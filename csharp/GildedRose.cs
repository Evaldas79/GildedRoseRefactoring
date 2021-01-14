using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        /* Original method
        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != "Aged Brie")
                    {
                        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }
        }

    */


        /// <summary>
        /// Refactored method by Evaldas.
        /// Changes:
        /// - I use 'foreach' instead of 'for'
        /// - I use 'switch' in case of many conditions instead of 'if + else if'
        /// - I calculate delta values before updating Item properties
        /// </summary>
        public void UpdateQuality()
        {
            foreach (Item item in this.Items)
            {
                int deltaSellIn = -1; // Default SellIn change (-1 => decrease by 1 day)
                int deltaQuality = 0; // Default Quality change


                #region --- SellIn update ---
                if (item.Name == "Sulfuras, Hand of Ragnaros")
                    deltaSellIn = 0;

                item.SellIn += deltaSellIn; // Changing item SellIn
                #endregion


                #region --- Quality update ---
                switch (item.Name)
                {
                    case "Aged Brie": // item whose quality increases in a normal way
                        deltaQuality = 1;
                        if (item.SellIn < 0)
                            deltaQuality = 2;
                        break;

                    case "Backstage passes to a TAFKAL80ETC concert": // item whose quality increases in a smart way
                        if (item.SellIn >= 10)
                            deltaQuality = 1;
                        else if (item.SellIn >= 5)
                            deltaQuality = 2;
                        else if (item.SellIn >= 0)
                            deltaQuality = 3;
                        else
                            deltaQuality = -item.Quality; // Quality will be set to 0
                        break;

                    case "Sulfuras, Hand of Ragnaros": // item with stable quality
                        deltaQuality = 0;
                        break;

                    case "Conjured Mana Cake": // item whose quality degrade twice as fast
                        deltaQuality = -2;
                        if (item.SellIn < 0)
                            deltaQuality = -4;
                        break;

                    default: // item whose quality dicreases in normal way
                        deltaQuality = -1;
                        if (item.SellIn < 0)
                            deltaQuality = -2;
                        break;
                }


                // Changing quality of an item
                if (deltaQuality != 0)
                {
                    item.Quality += deltaQuality;
                    if (item.Quality < 0)
                        item.Quality = 0;
                    else if (item.Quality > 50)
                        item.Quality = 50;
                }
                #endregion
            }
        }
    }
}
