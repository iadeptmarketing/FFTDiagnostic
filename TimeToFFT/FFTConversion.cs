using System;
using System.Collections.Generic;

namespace TimeToFFT
{
    public class FFTConversion
    {
        private int n, nu;
        /// <summary>
        /// Converts TimeWave data to FFT
        /// </summary>
        /// <param name="TimeData"></param>
        /// <returns>Returns converted FFT amplitudes if timedata is provided in a power of 2 </returns>
        public double[] ConvertTimeDataToFFT(double[] TimeData)
        {
            if(TimeData==null || TimeData.Length==0)
            {
                throw new ArgumentNullException("TimeData");
            }
            if(!isPowerOfTwo(TimeData.Length))
            {
                throw new ArithmeticException("TimeData is not correct");
            }
            return CalculateFFTMag(TimeData);
        }
        private bool isPowerOfTwo(int n)
        {

            if (n == 0)
                return false;

            return (int)(Math.Ceiling((Math.Log(n) /
                                       Math.Log(2)))) ==
                   (int)(Math.Floor(((Math.Log(n) /
                                      Math.Log(2)))));
        }
        private double[] CalculateFFTMag(double[] yData)
        {

            // assume n is a power of 2
            if (yData.Length % 2 == 0)
            {
                n = yData.Length;
            }
            else
            {
                n = yData.Length - 1;
            }
            nu = (int)(Math.Log(n) / Math.Log(2));
            int n2 = n / 2;
            int nu1 = nu - 1;
            double[] xre = new double[n];
            double[] xim = new double[n];
            double[] mag = new double[n2];
            double tr, ti, p, arg, c, s;
            try
            {
                for (int i = 0; i < n; i++)
                {
                    xre[i] = yData[i];
                    xim[i] = 0.0f;
                }
                int k = 0;

                for (int l = 1; l <= nu; l++)
                {
                    while (k < n)
                    {
                        for (int i = 1; i <= n2; i++)
                        {
                            try
                            {
                                p = bitrev(k >> nu1);
                                arg = 2 * (double)Math.PI * p / n;
                                c = (double)Math.Cos(arg);
                                s = (double)Math.Sin(arg);
                                tr = xre[k + n2] * c + xim[k + n2] * s;
                                ti = xim[k + n2] * c - xre[k + n2] * s;
                                xre[k + n2] = xre[k] - tr;
                                xim[k + n2] = xim[k] - ti;
                                xre[k] += tr;
                                xim[k] += ti;
                                k++;
                            }
                            catch
                            {
                                //break;
                            }
                        }
                        k += n2;
                    }
                    k = 0;
                    nu1--;
                    n2 = n2 / 2;
                }
                k = 0;
                int r;
                while (k < n)
                {
                    r = bitrev(k);
                    if (r > k)
                    {
                        tr = xre[k];
                        ti = xim[k];
                        xre[k] = xre[r];
                        xim[k] = xim[r];
                        xre[r] = tr;
                        xim[r] = ti;
                    }
                    k++;
                }

                mag[0] = 0;// (double)(Math.Sqrt(xre[0] * xre[0] + xim[0] * xim[0])) / n;
                for (int i = 1; i < n / 2; i++)
                {
                    //double temp_mag = (double)(Math.Sqrt(xre[i] * xre[i] + xim[i] * xim[i])) / 1000;
                    //double temp_2Per_mag = (2 * temp_mag) / 100;

                    //mag[i] = (float)(Math.Sqrt(xre[i] * xre[i] + xim[i] * xim[i])) / 1000;
                    //mag[i] = temp_mag - temp_2Per_mag;
                    mag[i] = (float)((2 * (float)(Math.Sqrt(xre[i] * xre[i] + xim[i] * xim[i]))) / n);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mag;
        }

        private int bitrev(int j)
        {

            int j2;
            int j1 = j;
            int k = 0;
            try
            {
                for (int i = 1; i <= nu; i++)
                {
                    j2 = j1 / 2;
                    k = 2 * k + j1 - 2 * j2;
                    j1 = j2;
                }
            }
            catch (Exception ex)
            {
                //ErrorLog_Class.ErrorLogEntry(ex);
            }
            return k;
        }
    }
}
