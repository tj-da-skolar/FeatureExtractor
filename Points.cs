using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureExtractor
{
    class Points
    {
        public static int TIMESTAMP = 0;
        public static int FRAME_NO = 1;
        public static int HEAD_X = 2;
        public static int HEAD_Y = 3;
        public static int HEAD_Z = 4;
        public static int SHOULDER_LEFT_X = 5;
        public static int SHOULDER_LEFT_Y = 6;
        public static int SHOULDER_LEFT_Z = 7;
        public static int SHOULDER_RIGHT_X = 8;
        public static int SHOULDER_RIGHT_Y = 9;
        public static int SHOULDER_RIGHT_Z = 10;
        public static int KNEE_LEFT_X = 11;
        public static int KNEE_LEFT_Y = 12;
        public static int KNEE_LEFT_Z = 13;
        public static int KNEE_RIGHT_X = 14;
        public static int KNEE_RIGHT_Y = 15;
        public static int KNEE_RIGHT_Z = 16;
        public static int ELBOW_LEFT_X = 17;
        public static int ELBOW_LEFT_Y = 18;
        public static int ELBOW_LEFT_Z = 19;
        public static int ELBOW_RIGHT_X = 20;
        public static int ELBOW_RIGHT_Y = 21;
        public static int ELBOW_RIGHT_Z = 22;
        public static int SPINE_X = 23;
        public static int SPINE_Y = 24;
        public static int SPINE_Z = 25;
        public static int HAND_LEFT_X = 26;
        public static int HAND_LEFT_Y = 27;
        public static int HAND_LEFT_Z = 28;
        public static int HAND_RIGHT_X = 29;
        public static int HAND_RIGHT_Y = 30;
        public static int HAND_RIGHT_Z = 31;
        public static int FOOT_LEFT_X = 32;
        public static int FOOT_LEFT_Y = 33;
        public static int FOOT_LEFT_Z = 34;
        public static int FOOT_RIGHT_X = 35;
        public static int FOOT_RIGHT_Y = 36;
        public static int FOOT_RIGHT_Z = 37;

        //When referencing points, always add 1 to the index
        public float[] arr = new float[38];

        public float csX = 0;
        public float csY = 0;
        public float csZ = 0;

        public Points(string line)
        {
            int CSVindex = 0;
            int arrIndex = 0;

            float temp = 0;
            string stringTemp = "";

            while(CSVindex < line.Length) {

                while (CSVindex < line.Length && line.ToCharArray(0, line.Length)[CSVindex] != ',')
                {
                    stringTemp += line.ToCharArray(0, line.Length)[CSVindex];
                    CSVindex++;
                }
                CSVindex++;
                temp = float.Parse(stringTemp);
                arr[arrIndex] = temp;
                arrIndex++;
                stringTemp = "";
            }

            csX = Math.Abs(arr[SHOULDER_LEFT_X] - arr[SHOULDER_RIGHT_X]) / 2;
            csY = Math.Abs(arr[SHOULDER_LEFT_Y] - arr[SHOULDER_RIGHT_Y]) / 2;
            csZ = Math.Abs(arr[SHOULDER_LEFT_Z] - arr[SHOULDER_RIGHT_Z]) / 2;
        }

        public float distanceFormula(float x1, float x2, float y1, float y2)
        {
            return (float)Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
        }

        public double headTilting(Points points)
        {
            float p1 = (float)Math.Pow(distanceFormula(points.csX, points.arr[Points.HEAD_X], points.csY, points.arr[Points.HEAD_Y]), 2);

            float p2 = (float)Math.Pow(distanceFormula(points.csX, points.arr[Points.SHOULDER_RIGHT_X], points.csY, points.arr[Points.SHOULDER_RIGHT_Y]), 2);

            float p3 = (float)Math.Pow(distanceFormula(points.arr[Points.HEAD_X], points.arr[Points.SHOULDER_RIGHT_X], points.arr[Points.HEAD_Y], points.arr[Points.SHOULDER_RIGHT_Y]), 2);

            float p4 = distanceFormula(points.csX, points.arr[Points.HEAD_X], points.csY, points.arr[Points.HEAD_Y]);

            float p5 = distanceFormula(points.csX, points.arr[Points.SHOULDER_RIGHT_X], points.csY, points.arr[Points.SHOULDER_RIGHT_Y]);

            return Math.Acos((p1 + p2 + p3) / (2 * p4 * p5));
        }

        public string bodyMovement(Points points, Points pointsPrev)
        {
            if ((points.arr[Points.SPINE_Z] < pointsPrev.arr[Points.SPINE_Z]))
                return "Forward Movement";
            else
                if ((points.arr[Points.SPINE_Z] > pointsPrev.arr[Points.SPINE_Z]))
                    return "Backward Movement";
                else
                    return "No Movement";
        }

        public string leaning(Points points, Points pointsPrev)
        {
            if ((points.csZ <= pointsPrev.csZ))
                return "Leaning forward";
            else
                if ((points.csZ > pointsPrev.csZ))
                    return "Leaning backward";
                else
                    return "No lean";
        }
    }
}
