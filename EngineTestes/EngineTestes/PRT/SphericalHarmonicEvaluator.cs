using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Utils;
using Microsoft.Xna.Framework;

namespace EngineTestes.PRT
{
    public struct Spherical
    {
        public  float theta;
        public float phi;
    }
    public struct Sample
    {
        public  Spherical spherical_coord;
        public  Vector3 cartesian_coord;
        public  float[] sh_functions;
    };

    public struct Sampler
    {
        public Sample[] samples;
        public int number_of_samples;

        void GenerateSamples(int N)
        {
        this.samples = new Sample[N * N];
        this.number_of_samples = N*N;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                float a = ((float) i) + Random() / (float) N;
                float b = ((float) j) + Random() / (float) N;
                float theta = (float) (2* Math.Acos(Math.Sqrt(1-a)));
                float phi = (float)(2*Math.PI*b);
                float x = (float) (Math.Sin(theta)*Math.Cos(phi));
                float y = (float) (Math.Sin(theta)*Math.Sin(phi));
                float z = (float)Math.Cos(theta);
                int k = i*N + j;
                this.samples[k].spherical_coord.theta = theta;
                this.samples[k].spherical_coord.phi = phi;
                this.samples[k].cartesian_coord.X = x;
                this.samples[k].cartesian_coord.Y = y;
                this.samples[k].cartesian_coord.Z = z;
                this.samples[k].sh_functions = null;
            }
        }
        }

        float Random()
        {
        float random = (float) (StaticRandom.Random() % 1000) / 1000.0f;
        return(random);
        }

    }

    public class SphericalHarmonicEvaluator
    {
        float Legendre(int l, int m, float x)
        {            
            float result;
            if (l == m+1)
            {
                result = x*(2*m + 1)*Legendre(m, m,x);
            }
            else if (l == m)
            {
                float p1 = (float)Math.Pow(-1, m);
                float p2 = DoubleFactorial(2*m - 1);
                float p3 = (float)Math.Pow((1 - x * x), m / 2);
                result = p1 * p2 * p3;                    
            }
            else
            {
                result = (x*(2*l-1)*Legendre(l-1, m,x) - (l+m-1)*Legendre(l-2, m,x))/(l-m);
            }

            return(result);
        }

        float SphericalHarmonic(int l, int m, float theta, float phi)
        {
            float result;
            if (m > 0)
                result = (float)(Math.Sqrt(2) * K(l, m) * Math.Cos(m * phi) * Legendre(l, m, (float)Math.Cos(theta)));
            else if (m < 0)
                result = (float)(Math.Sqrt(2) * K(l, m) * Math.Sin(-m * phi) * Legendre(l, -m, (float) Math.Cos(theta)));
            else
                result = K(l, m) * Legendre(l, 0, (float) Math.Cos(theta));
            return (result);
        }

        float K(int l, int m)
        {
            float num = (2 * l + 1) * Factorial(l - Math.Abs(m));
            float denom = (float) (4 * Math.PI * Factorial(l + Math.Abs(m)));
            float result = (float) (Math.Sqrt(num / denom));
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


        public void PrecomputeSHFunctions(Sampler sampler, int bands)
        {
            for (int i = 0; i < sampler.number_of_samples; i++)
            {
                float[] sh_functions = new float[bands * bands];
                sampler.samples[i].sh_functions = sh_functions;
                float theta = sampler.samples[i].spherical_coord.theta;
                float phi = sampler.samples[i].spherical_coord.phi;
                for (int l = 0; l < bands; l++)
                    for (int m = -l; m <= l; m++)
                    {
                        int j = l * (l + 1) + m;
                        sh_functions[j] = SphericalHarmonic(l, m, theta, phi);
                    }
            }
        }

        float DoubleFactorial(int n)
        {
        if (n <= 1)
        return(1);
        else
        return(n * DoubleFactorial(n-2));
        }

    }
}
