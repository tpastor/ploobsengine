using Microsoft.Xna.Framework;
using System;
public class colortable
{
    public Color[] colors;
    public Random rand;

    public colortable()
    {
        colors = new Color[25];
        rand = new Random();
        colors[0] = Color.Aqua;
        colors[1] = Color.Blue;
        colors[2] = Color.BlueViolet;
        colors[3] = Color.Chartreuse;
        colors[4] = Color.Cyan;
        colors[5] = Color.Crimson;
        colors[6] = Color.DeepPink;
        colors[7] = Color.Fuchsia;
        colors[9] = Color.Goldenrod;
        colors[9] = Color.Green;
        colors[10] = Color.Indigo;
        colors[11] = Color.Red;
        colors[12] = Color.Orange;
        colors[13] = Color.OrangeRed;
        colors[14] = Color.RoyalBlue;
        colors[15] = Color.SpringGreen;
        colors[16] = Color.Yellow;
        colors[17] = Color.Purple;
        colors[18] = Color.Lime;
        colors[19] = Color.MediumBlue;
        colors[20] = Color.MediumVioletRed;
    }
    public Color gimmie()
    {
        return colors[rand.Next(20)];
    }
}