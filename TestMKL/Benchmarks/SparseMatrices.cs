﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKLtest.Benchmarks
{
    class SparseMatrices
    {
        public double[,] matrixPosdef = new double[,] {
            {  0.8977,   -0.1096,         0,         0,         0,         0,         0,         0,         0,    0.0250 },
            { -0.1096,    0.8382,         0,         0,         0,         0,         0,         0,         0,   -0.1685 },
            {       0,         0,    0.5819,   -0.0060,         0,         0,   -0.0845,    0.0003,    0.0030,         0 },
            {       0,         0,   -0.0060,    0.4611,         0,         0,   -0.0291,    0.0002,    0.0015,         0 },
            {       0,         0,         0,         0,    0.2783,   -0.0023,         0,         0,         0,         0 },
            {       0,         0,         0,         0,   -0.0023,    0.3593,         0,         0,         0,         0 },
            {       0,         0,   -0.0845,   -0.0291,         0,         0,    0.1924,    0.0015,    0.0143,         0 },
            {       0,         0,    0.0003,    0.0002,         0,         0,    0.0015,    0.1301,    0.0087,         0 },
            {       0,         0,    0.0030,    0.0015,         0,         0,    0.0143,    0.0087,    0.2095,         0 },
            {  0.0250,   -0.1685,         0,         0,         0,         0,         0,         0,         0,    0.1384 }};

        public double[,] matrixInvertible = new double[,] {
            { 0.3747, 0.4369, 0.3043, 0, 0.2909, 0.2425, 0, 0, 0, 0 },
            {      0,         0,    0.9367,         0,         0,         0,         0,         0,         0,    0.8602},
            {      0,         0,    0.3972,         0,    0.4794,         0,    0.5650,    0.4896,    0.2698,         0},
            {      0,         0,         0,         0,    0.9897,    0.1837,         0,         0,         0,         0},
            { 0.8617,         0,         0,    0.0326,         0,         0,         0,    0.3320,         0,    0.7487},
            {      0,         0,    0.6444,    0.1692,         0,         0,         0,    0.9522,         0,         0},
            {      0,         0,         0,         0,    0.5433,         0,    0.2514,    0.5786,         0,         0},
            { 0.9155,         0,         0,    0.8956,         0,         0,         0,         0,         0,         0},
            {      0,         0,    0.4825,         0,         0,    0.4427,         0,    0.3118,    0.0553,         0},
            {      0,    0.7538,         0,         0,         0,         0,    0.1319,    0.3559,         0,         0}};

        public double[,] matrixSingular = new double[,] {
            { 0.9933,    0.0936,    0.2439,         0,    0.2152,         0,         0,    0.2439,         0,         0 },
            {      0,         0,         0,         0,         0,         0,         0,         0,    0.3397,         0 },
            {      0,    0.1979,    0.5068,         0,         0,         0,         0,    0.5068,    0.9508,         0 },
            {      0,         0,         0,         0,    0.5845,         0,         0,         0,    0.6065,         0 },
            {      0,    0.7146,         0,         0,         0,         0,    0.4015,         0,    0.8587,    0.9205 },
            {      0,         0,    0.2856,    0.2856,    0.7968,         0,         0,         0,    0.1428,         0 },
            {      0,         0,    0.3833,         0,         0,    0.6107,    0.7038,    0.3833,         0,    0.7287 },
            { 0.8873,         0,    0.0558,    0.0558,         0,         0,         0,         0,    0.1382,         0 },
            {      0,    0.8631,         0,         0,         0,    0.4217,         0,         0,         0,         0 },
            {      0,         0,         0,         0,         0,    0.4113,         0,         0,         0,    0.9591 }};

        public double[] x = new double[] { 7.5025, 9.8100, 2.3352, 0.9623, 3.8458, 5.0027, 5.7026, 9.7663, 4.9286, 4.0088 };
        public double[] matrixPosdef_x = new double[] { 5.7600, 6.7250, 0.8889, 0.2731, 1.0588, 1.7886, 0.9570, 1.3229, 1.2075, -0.9106 };
        public double[] matrixInvertible_x = new double[] { 10.1397, 5.6358, 12.1045, 4.7252, 12.7401, 10.9671, 9.1738, 7.7304, 6.6591, 11.6228 };
        public double[] matrixSingular_x = new double[] { 12.1496, 1.6742, 12.7606, 5.2371, 17.2221, 4.7099, 14.6284, 7.5221, 10.5766, 5.9025 };
    }
}
