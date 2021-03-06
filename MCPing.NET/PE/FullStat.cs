﻿using System.Collections.Generic;
using System.Text;

namespace nao20010128nao.MCPing.PE
{
    public class FullStat : ServerPingResult
    {
        private Dictionary<string, string> datas = new Dictionary<string, string>();
        private List<string> playerList = new List<string>();

        public FullStat(byte[] data)
        {
            data = Utils.trim(data);
            byte[][] temp = Utils.split(data);
            byte[] d;
            int dataEnds = 0;
            for (int i = 2; i < temp.Length; i += 1)
            {
                if ((d = temp[i]).Length == 0 ? false : d[0] == 1)
                {
                    dataEnds = i;
                    break;
                }
            }

            if ((dataEnds % 2) == 0)
                dataEnds--;

            for (int i = 2; i < dataEnds; i += 2)
            {
                string k = new UTF8Encoding().GetString(temp[i]);
                string v = new UTF8Encoding().GetString(temp[i + 1]);
                if ("" == k | "" == v)
                {
                    continue;
                }
                datas[k] = v;
            }

            playerList = new List<string>();//
            for (int i = dataEnds + 2; i < temp.Length; i++)
            {
                playerList.Add(new UTF8Encoding().GetString(temp[i]));
            }
        }

        public IDictionary<string, string> Data
        {
            get
            {
                return new Dictionary<string, string>(datas);
            }
        }

        public IList<string> Players
        {
            get
            {
                return playerList.AsReadOnly();
            }
        }
    }
}
