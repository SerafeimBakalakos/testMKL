﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMKL.Benchmarks
{
    static class TriangularMatrices
    {
        public const int order = 10;

        //Non singular
        public static double[,] lower = new double[,] {
            {4.2289,         0,         0,         0,         0,         0,         0,         0,         0,         0},
            {0.9423,    6.5445,         0,         0,         0,         0,         0,         0,         0,         0},
            {5.9852,    4.0762,    0.9082,         0,         0,         0,         0,         0,         0,         0},
            {4.7092,    8.1998,    2.6647,    9.5769,         0,         0,         0,         0,         0,         0},
            {6.9595,    7.1836,    1.5366,    2.4071,    3.4446,         0,         0,         0,         0,         0},
            {6.9989,    9.6865,    2.8101,    6.7612,    7.8052,    7.7016,         0,         0,         0,         0},
            {6.3853,    5.3133,    4.4009,    2.8906,    6.7533,    3.2247,    1.9175,         0,         0,         0},
            {0.3360,    3.2515,    5.2714,    6.7181,    0.0672,    7.8474,    7.3843,    5.4659,         0,         0},
            {0.6881,    1.0563,    4.5742,    6.9514,    6.0217,    4.7136,    2.4285,    4.2573,    6.0730,         0},
            {3.1960,    6.1096,    8.7537,    0.6799,    3.8677,    0.3576,    9.1742,    6.4444,    4.5014,    6.1346}};

        //Singular
        public static double[,] lowerSing = new double[,] {
            {4.2289,         0,         0,         0,         0,         0,         0,         0,         0,         0},
            {0.9423,    6.5445,         0,         0,         0,         0,         0,         0,         0,         0},
            {5.9852,    4.0762,    0.9082,         0,         0,         0,         0,         0,         0,         0},
            {4.7092,    8.1998,    2.6647,    9.5769,         0,         0,         0,         0,         0,         0},
            {6.9595,    7.1836,    1.5366,    2.4071,    3.4446,         0,         0,         0,         0,         0},
            {6.9595,    7.1836,    1.5366,    2.4071,    3.4446,         0,         0,         0,         0,         0},
            {6.3853,    5.3133,    4.4009,    2.8906,    6.7533,    3.2247,    1.9175,         0,         0,         0},
            {0.3360,    3.2515,    5.2714,    6.7181,    0.0672,    7.8474,    7.3843,    5.4659,         0,         0},
            {0.6881,    1.0563,    4.5742,    6.9514,    6.0217,    4.7136,    2.4285,    4.2573,    6.0730,         0},
            {3.1960,    6.1096,    8.7537,    0.6799,    3.8677,    0.3576,    9.1742,    6.4444,    4.5014,    6.1346}};

        //Non singular
        public static double[,] upper = new double[,] {
            {4.2289,    5.3086,   7.7880,    5.1805,    2.5479,    9.1599,    1.7587,    2.6906,    6.4762,    4.5873},
            { 0,        6.5445,   4.2345,    9.4362,    2.2404,    0.0115,    7.2176,    7.6550,    6.7902,    6.6194},
            { 0,         0,       0.9082,    6.3771,    6.6783,    4.6245,    4.7349,    1.8866,    6.3579,    7.7029},
            { 0,         0,        0,    	 9.5769,    8.4439,    4.2435,    1.5272,    2.8750,    9.4517,    3.5022},
            { 0,         0,        0,        0,    	    3.4446,    4.6092,    3.4112,    0.9111,    2.0893,    6.6201},
            { 0,         0,        0,        0,         0,    	   7.7016,    6.0739,    5.7621,    7.0928,    4.1616},
            { 0,         0,        0,        0,         0,          0,    	  1.9175,    6.8336,    2.3623,    8.4193},
            { 0,         0,        0,        0,         0,          0,        0,  	     5.4659,    1.1940,    8.3292},
            { 0,         0,        0,        0,         0,          0,        0,         0,    		6.0730,    2.5644},
            { 0,         0,        0,        0,         0,          0,        0,         0,         0,    	   6.1346}};

        //Singular
        public static double[,] upperSing = new double[,] {
            {4.2289,    5.3086,   7.7880,    5.1805,    2.5479,    9.1599,    1.7587,    2.6906,    6.4762,    4.5873},
            { 0,        6.5445,   4.2345,    9.4362,    2.2404,    0.0115,    7.2176,    7.6550,    6.7902,    6.6194},
            { 0,         0,       0.0000,    6.3771,    6.6783,    4.6245,    4.7349,    1.8866,    6.3579,    7.7029},
            { 0,         0,        0,        9.5769,    8.4439,    4.2435,    1.5272,    2.8750,    9.4517,    3.5022},
            { 0,         0,        0,        0,         3.4446,    4.6092,    3.4112,    0.9111,    2.0893,    6.6201},
            { 0,         0,        0,        0,         0,         7.7016,    6.0739,    5.7621,    7.0928,    4.1616},
            { 0,         0,        0,        0,         0,         0,         1.9175,    6.8336,    2.3623,    8.4193},
            { 0,         0,        0,        0,         0,         0,         0,         5.4659,    1.1940,    8.3292},
            { 0,         0,        0,        0,         0,         0,         0,         0,         6.0730,    2.5644},
            { 0,         0,        0,        0,         0,         0,         0,         0,         0,         6.1346}};

        public static double[] x = 
            new double[] { 0.5822, 0.5407, 0.8699, 0.2648, 0.3181, 0.1192, 0.9398, 0.6456, 0.4795, 0.6393 };

        public static double[] lower_x = Utilities.MatrixTimesVector(lower, x);
        public static double[] lowerSing_x = Utilities.MatrixTimesVector(lowerSing, x);
        public static double[] upper_x = Utilities.MatrixTimesVector(upper, x);
        public static double[] upperSing_x = Utilities.MatrixTimesVector(upperSing, x);

        //public static double[] lower_x = new double[] { 2.4621, 4.0872, 6.4786, 12.0293, 11.0058, 16.9480, 15.5189, 19.7436, 17.2118, 33.0947 };
        //public static double[] lowerSing_x = new double[] { 2.4621, 4.0872, 6.4786, 12.0293, 11.0058, 11.0058, 15.5189, 19.7436, 17.2118, 33.0947 };
        //public static double[] upper_x = new double[] { 24.8092, 29.6478, 18.7952, 15.7902, 10.6732, 16.4078, 12.7290, 9.4262, 4.5514, 3.9218 };
        //public static double[] upperSing_x = new double[] { 24.8092, 29.6478, 18.0052, 15.7902, 10.6732, 16.4078, 12.7290, 9.4262, 4.5514, 3.9218 };
    }
}
