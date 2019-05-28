﻿using System;

namespace EddieToNewFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Bridge b = new Bridge("1.0");
            b.Initialise();
            b.TransposeContents();

            Console.ReadLine();
        }
    }
}
