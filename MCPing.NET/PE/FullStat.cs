﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nao20010128nao.MCPing.PE
{
    public class FullStat : ServerPingResult
    {
        static byte NULL = 00;
        static byte SPACE = 20;

        private Dictionary<String, String> datas = new Dictionary<String, String>();
        private List<String> playerList = new List<String>();

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
                String k = new UTF8Encoding().GetString(temp[i]);
                String v = new UTF8Encoding().GetString(temp[i + 1]);
                if ("" == k | "" == v)
                {
                    continue;
                }
                datas[k] = v;
            }

            playerList = new List<String>();//
            for (int i = dataEnds + 2; i < temp.Length; i++)
            {
                playerList.Add(new UTF8Encoding().GetString(temp[i]));
            }
        }

        public IDictionary<String, String> getData()
        {
            return new Dictionary<String, String>(datas);
        }

        public IList<String> getPlayerList()
        {
            return playerList.AsReadOnly();
        }

    }
}