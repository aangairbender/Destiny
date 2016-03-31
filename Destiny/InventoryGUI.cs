using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class InventoryGUI
    {
        public bool visible;
        private World world;
        public const float blockSize = 0.3f;
        public const float padding = 0.05f;
        public const float padding2 = 0.2f;
        public const float listElHeight = 35;
        public const float buttonWidth = 0.2f;
        public const float buttonHeight = 0.05f;
        public const int offset = 2;
        private Item chosenItem = null;
        public InventoryGUI(World world)
        {
            visible = false;
            this.world = world;
        }
        public void show()
        {
            visible = true;
        }
        public void hide()
        {
            visible = false;
        }

        public void draw(Graphics g, int width, int height)
        {
            drawItemList(g, width * (1.0f - padding - blockSize * 1.0f), height * padding, width * blockSize, height * (1.0f - 2.0f * padding));
            drawItemInfo(g, width * (1.0f - padding - blockSize * 2.0f), height * padding, width * blockSize, height * (1.0f - 2.0f * padding));
            drawHeroInfo(g, width * (1.0f - padding - blockSize * 3.0f), height * padding, width * blockSize, height * (1.0f - 2.0f * padding));
           
        }

        private void drawHeroInfo(Graphics g, float x, float y, float width, float height)
        {
            g.FillRectangle(new SolidBrush(Color.BurlyWood), x, y, width, height);
            g.DrawRectangle(new Pen(Color.Black, 4.0f), x, y, width, height);

            Font titleFont = new Font("Arial", 20);
            String title = world.hero.name;
            SizeF titleBox = g.MeasureString(title, titleFont);
            g.DrawString(title, titleFont, new SolidBrush(Color.Brown), new PointF(x + (width - titleBox.Width) / 2.0f, y + height * padding));

            Font elFont = new Font("Arial", 16);
            Brush elBrush = new SolidBrush(Color.DarkBlue);
            Brush slotBrush = new SolidBrush(Color.DarkGreen);
            float listX = x + width * padding;
            float listY = y + height * padding * 2.0f + titleBox.Height;
            for (int i = 0; i < world.hero.slot.Count && listY + listElHeight * i * 2 < y + height * (1.0 - padding); i++)
            {
                SizeF elBox = g.MeasureString(Utils.getSlotName(i), elFont);
                
                g.DrawString(Utils.getSlotName(i), elFont, slotBrush, listX + offset + width * (1.0f - 2.0f * padding) * padding, listY + listElHeight * i * 2 + (listElHeight - elBox.Height) / 2.0f);
                String elName = "Пусто";
                if (world.hero.slot[i] != null) elName = world.hero.slot[i].name;
                elBox = g.MeasureString(elName, elFont);
                g.DrawRectangle(new Pen(Color.Black, 2), listX, listY + listElHeight * (i * 2 + 1), width * (1.0f - 2.0f * padding), listElHeight);
                g.FillRectangle(new SolidBrush(Color.SandyBrown), listX, listY + listElHeight * (i * 2 + 1), width * (1.0f - 2.0f * padding), listElHeight);

                g.DrawString(elName, elFont, elBrush, listX + offset + width * (1.0f - 2.0f * padding) * padding, listY + listElHeight * (i * 2 + 1) + (listElHeight - elBox.Height) / 2.0f);
                if (world.inputController.mouseDowned)
                {
                    float tx = listX;
                    float ty = listY + listElHeight * (2*i + 1);
                    float elwidth = width * (1.0f - 2.0f * padding);
                    float elheight = listElHeight;
                    if (new RectangleF(tx, ty, elwidth, elheight).Contains(world.inputController.mouseLocation))
                    {
                        chosenItem = world.hero.slot[i];
                    }

                }
            }
        }

        private void drawItemInfo(Graphics g, float x, float y, float width, float height)
        {
            if (chosenItem == null) return;
            g.FillRectangle(new SolidBrush(Color.BurlyWood), x, y, width, height);
            g.DrawRectangle(new Pen(Color.Black, 4.0f), x, y, width, height);
            Font titleFont = new Font("Arial", 20);
            String title = chosenItem.name;
            SizeF titleBox = g.MeasureString(title, titleFont);

            g.FillRectangle(new SolidBrush(Color.White), x + (width - titleBox.Width - world.bc["items"][chosenItem.sprite].Width) / 2.0f, y + height * padding, world.bc["items"][chosenItem.sprite].Width, world.bc["items"][chosenItem.sprite].Height);
            g.DrawImage(world.bc["items"][chosenItem.sprite], x + (width - titleBox.Width - world.bc["items"][chosenItem.sprite].Width) / 2.0f, y + height * padding);
            g.DrawRectangle(new Pen(Color.Black, 1), x + (width - titleBox.Width - world.bc["items"][chosenItem.sprite].Width) / 2.0f, y + height * padding, world.bc["items"][chosenItem.sprite].Width, world.bc["items"][chosenItem.sprite].Height);
                

            g.DrawString(title, titleFont, new SolidBrush(Color.Brown), new PointF(x + offset + (width - titleBox.Width + world.bc["items"][chosenItem.sprite].Width) / 2.0f, y + height * padding));

            float descX = x + width * padding;
            float descY = y + height * padding * 2.0f + titleBox.Height;
            Font descFont = new Font("Arial", 14);
            g.DrawString(chosenItem.description, descFont, new SolidBrush(Color.DarkCyan),new RectangleF(descX, descY, width*(1.0f-2.0f*padding),height*(1.0f-3.0f*padding-buttonHeight)));

            if (chosenItem.canEquip(world.hero) && !(world.hero.slot[chosenItem.slot]!=null && world.hero.slot[chosenItem.slot]!=chosenItem))
            {
                float btn1X = x + width * padding2;
                float btn1Y = y + height*(1.0f - padding - buttonHeight);
                String text1 = "Надеть";
                if (chosenItem.canEquip(world.hero) && world.hero.slot[chosenItem.slot] == chosenItem) text1 = "Снять";
                drawButton(g, btn1X, btn1Y, buttonWidth * width, buttonHeight * height, text1);
                if (world.inputController.mouseDowned && new RectangleF(btn1X, btn1Y, buttonWidth*width, buttonHeight*height).Contains(world.inputController.mouseLocation))
                {
                    if (text1 == "Надеть")
                    {
                        chosenItem.equip(world.hero);
                    }
                    else
                    {
                        chosenItem.unequip(world.hero);
                    }
                }
            }
            float btn2X = x + width * (1.0f - padding2) - buttonWidth*width;
            float btn2Y = y + height * (1.0f - padding - buttonHeight);
            String text2 = "Выбросить";
            drawButton(g, btn2X, btn2Y, buttonWidth * width, buttonHeight * height, text2);
            if (world.inputController.mouseDowned && new RectangleF(btn2X, btn2Y, buttonWidth*width, buttonHeight*height).Contains(world.inputController.mouseLocation))
            {
                if (chosenItem.canEquip(world.hero) && world.hero.slot[chosenItem.slot] == chosenItem)
                {
                    chosenItem.unequip(world.hero);
                }
                world.hero.inventory.Remove(chosenItem);
                world.map[world.hero.location.X, world.hero.location.Y].itemsLying.Add(chosenItem);
                chosenItem = null;
            }
        }

        private void drawButton(Graphics g, float x, float y, float width, float height, String text)
        {
            Font textFont = new Font("Arial", 16);
            SizeF textBox = g.MeasureString(text, textFont);
            if (textBox.Width > width)
            {
                width = textBox.Width;
            }
            if(textBox.Height > height)
            {
                height = textBox.Height;
            }
            g.FillRectangle(new SolidBrush(Color.LightCoral), x, y, width, height);
            g.DrawRectangle(new Pen(Color.Black,2), x, y, width, height);
            g.DrawString(text, textFont, new SolidBrush(Color.Black), x + (width - textBox.Width) / 2.0f, y + (height - textBox.Height) / 2.0f); 
        }

        private void drawItemList(Graphics g, float x, float y, float width, float height)
        {
            g.FillRectangle(new SolidBrush(Color.BurlyWood), x, y, width, height);
            g.DrawRectangle(new Pen(Color.Black, 4.0f), x, y, width, height);

            Font titleFont = new Font("Arial", 20);
            String title = "Инвентарь";
            SizeF titleBox = g.MeasureString(title, titleFont);
            g.DrawString(title, titleFont, new SolidBrush(Color.Brown),new PointF(x+(width-titleBox.Width)/2.0f,y+height*padding));

            Font elFont = new Font("Arial", 16);
            Brush elBrush = new SolidBrush(Color.DarkBlue);
            float listX = x + width * padding;
            float listY = y + height * padding * 2.0f + titleBox.Height;
            for(int i=0;i<world.hero.inventory.Count && listY + listElHeight*i < y + height*(1.0 - padding);++i)
            {
                g.DrawRectangle(new Pen(Color.Black, 2), listX, listY + listElHeight * i, width * (1.0f - 2.0f * padding), listElHeight);
                g.FillRectangle(new SolidBrush(Color.SandyBrown), listX, listY + listElHeight * i, width * (1.0f - 2.0f * padding), listElHeight);
                SizeF elBox = g.MeasureString(world.hero.inventory[i].name, elFont);
                g.FillRectangle(new SolidBrush(Color.White), listX + offset, listY + listElHeight * i + (listElHeight - world.bc["items"][world.hero.inventory[i].sprite].Height) / 2.0f, world.bc["items"][world.hero.inventory[i].sprite].Width, world.bc["items"][world.hero.inventory[i].sprite].Height);
                g.DrawImage(world.bc["items"][world.hero.inventory[i].sprite], listX + offset, listY + listElHeight * i + (listElHeight - world.bc["items"][world.hero.inventory[i].sprite].Height) / 2.0f);
                g.DrawRectangle(new Pen(Color.Black, 1), listX+offset, listY + listElHeight * i + (listElHeight - world.bc["items"][world.hero.inventory[i].sprite].Height) / 2.0f, world.bc["items"][world.hero.inventory[i].sprite].Width, world.bc["items"][world.hero.inventory[i].sprite].Height);
                g.DrawString(world.hero.inventory[i].name, elFont, elBrush, listX +offset+ world.bc["items"][world.hero.inventory[i].sprite].Width + width * (1.0f - 2.0f * padding) * padding, listY + listElHeight*i + (listElHeight - elBox.Height) / 2.0f);
                if (world.inputController.mouseDowned)
                {
                    float tx = listX;
                    float ty = listY + listElHeight * i;
                    float elwidth = width * (1.0f - 2.0f * padding);
                    float elheight = listElHeight;
                    if(new RectangleF(tx,ty,elwidth,elheight).Contains(world.inputController.mouseLocation))
                    {
                        chosenItem = world.hero.inventory[i];
                    }

                }
            }
        }


    }
}
