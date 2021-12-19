using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BleCommunication
{
    class PrevSensorData
    {
        public int sensorValueIdx = -1;
        public float time = -0.05f;
        public float gFx = 0.0f;
        public float gFy = 0.0f;
        public float gFz = 0.0f;

        public float wx = 0.0f;
        public float wy = 0.0f;
        public float wz = 0.0f;

        public float bx = 0.0f;
        public float by = 0.0f;
        public float bz = 0.0f;

        public void setData(int idx, float time,
            float gFx, float gFy, float gFz,
            float wx, float wy, float wz,
            float bx, float by, float bz)
        {
            this.sensorValueIdx = idx;
            this.time = time;
            this.gFx = gFx;
            this.gFy = gFy;
            this.gFz = gFz;

            this.wx = wx;
            this.wy = wy;
            this.wz = wz;

            this.bx = bx;
            this.by = by;
            this.bz = bz;
        }
        public void init()
        {
            this.sensorValueIdx = -1;
            this.time = -0.05f;
            this.gFx = 0.0f;
            this.gFy = 0.0f;
            this.gFz = 0.0f;

            this.wx = 0.0f;
            this.wy = 0.0f;
            this.wz = 0.0f;

            this.bx = 0.0f;
            this.by = 0.0f;
            this.bz = 0.0f;
        }
        public bool isBegin()
        {
            return this.sensorValueIdx == -1;
        }
        public int getInterval(int curIdx)
        {
            int interval = curIdx - this.sensorValueIdx;
            if (interval >= 0)
                return interval;
            return 256 - this.sensorValueIdx + curIdx;
        }
    }
}
