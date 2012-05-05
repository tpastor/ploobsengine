using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EngineTestes
{
    public class Sample
    {
        public float theta;
        public float phi;
        public float Dx;
        public float Dy;
        public float Dz;
        public List<float> vSHEval;

        public Sample(float a, float b, int bands)
        {
            float theta = 2.0f * (float)Math.Acos(Math.Sqrt(1.0f - a));
            float phi = 2.0f * (float)Math.PI * b;
            Dx = (float)(Math.Sin(theta) * Math.Cos(phi));
            Dy = (float)(Math.Sin(theta) * Math.Sin(phi));
            Dz = (float)(Math.Cos(theta));

            for (int l = 0; l < bands; l++)
            {
                for (int m = -l; m <= l; m++)
                {
                    vSHEval.Add(SphericalHarmonic(l, m, theta, phi));
                }
            }
        }

        float Legendre(int l, int m, float x)
        {
            float result;
            if (l == m + 1)
            {
                result = x * (2 * m + 1) * Legendre(m, m, x);
            }
            else if (l == m)
            {
                float p1 = (float)Math.Pow(-1, m);
                float p2 = DoubleFactorial(2 * m - 1);
                float p3 = (float)Math.Pow((1 - x * x), m / 2);
                result = p1 * p2 * p3;
            }
            else
            {
                result = (x * (2 * l - 1) * Legendre(l - 1, m, x) - (l + m - 1) * Legendre(l - 2, m, x)) / (l - m);
            }

            return (result);
        }

        float DoubleFactorial(int n)
        {
            if (n <= 1)
                return (1);
            else
                return (n * DoubleFactorial(n - 2));
        }

        float K(int l, int m)
        {
            float num = (2 * l + 1) * Factorial(l - Math.Abs(m));
            float denom = (float)(4 * Math.PI * Factorial(l + Math.Abs(m)));
            float result = (float)(Math.Sqrt(num / denom));
            return (result);
        }

        static int Factorial(int factor)
        {

            int factorial = 1;

            for (int i = 1; i <= factor; i++)
            {

                factorial *= i;

            }

            return factorial;

        }


        float SphericalHarmonic(int l, int m, float theta, float phi)
        {
            float result;
            if (m > 0)
                result = (float)(Math.Sqrt(2) * K(l, m) * Math.Cos(m * phi) * Legendre(l, m, (float)Math.Cos(theta)));
            else if (m < 0)
                result = (float)(Math.Sqrt(2) * K(l, m) * Math.Sin(-m * phi) * Legendre(l, -m, (float)Math.Cos(theta)));
            else
                result = K(l, m) * Legendre(l, 0, (float)Math.Cos(theta));
            return (result);
        }

    }
    
    public class Sampler
    {

        public int samples;
        public int bands;
        public List<Sample> vSamples;

        float Random()
        {
            float random = (float)(StaticRandom.Random() % 1000) / 1000.0f;
            return (random);
        }

        public Sampler(int N, int bands)
        {
            samples = N*N;
            this.bands = bands;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    float a = (i + Random()) / (float)N;
                    float b = (j + Random()) / (float)N;
                    vSamples.Add(new Sample(a, b, bands));
                }
            }
        }

        
    }

    public class LightProbeReader : ILightProbeReader
    {
        Texture2D image;
        Color[] color;
        public LightProbeReader(Texture2D image)
        {
            this.image = image;
            color = new Color[image.Width * image.Height];
            image.GetData<Color>(color);
        }

        public Color LightAccess(Vector3 direction)
        {
                double d = Math.Sqrt(direction.X*direction.X + direction.Y*direction.Y);
                double r = (d == 0) ? 0.0f : (1.0f/Math.PI/2.0f) * Math.Acos(direction.Z) / d;
                Vector2 tex_coord = new Vector2();
                tex_coord.X = (float) (0.5f + direction.X * r);
                tex_coord.Y  =(float) (0.5f + direction.Y * r);
                int x, y;
                x = (int) (tex_coord.X * image.Width);
                y = (int) (tex_coord.Y * image.Height);
                int pixel_index = x*image.Width + y;
                return color[pixel_index];                
        }
    }

    public class MonteCarlo
    {
        
        public List<Vector3> ProjectLight(ILightProbeReader lreader, Sampler sampler)
        {
            float weight = 4 * (float)Math.PI;
            int T = sampler.samples;
            float n = sampler.bands;
            List<Vector3> vResult = new List<Vector3>();
            for (int i = 0; i < n * n; i++)
            {
                vResult[i] = new Vector3();
            }
            for (int j = 0; j < T; j++)
            {
                for (int i = 0; i < n * n; i++)
                {
                    Sample sample = sampler.vSamples[i];
                    vResult[i] += lreader.LightAccess(new Vector3(sample.Dx,sample.Dy,sample.Dz)).ToVector3() * sample.vSHEval[i];
                }
            }
            for (int i = 0; i < n * n; i++)
            {
                vResult[i] *= weight / T;
            }
            return vResult;
        }


    }



}
