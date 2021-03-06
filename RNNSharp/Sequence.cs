﻿using System;
using System.Collections.Generic;

/// <summary>
/// RNNSharp written by Zhongkai Fu (fuzhongkai@gmail.com)
/// </summary>
namespace RNNSharp
{
    public class Sequence
    {
        public State[] States { get;}

        public int GetDenseDimension()
        {
            if (0 == States.Length || States[0].DenseData == null)
            {
                return 0;
            }
            else
            {
                return States[0].DenseData.GetDimension();
            }
        }

        public int GetSparseDimension()
        {
            if (0 == States.Length) return 0;
            else return States[0].SparseData.GetDimension();
        }

        public void SetLabel(Sentence sent, TagSet tagSet)
        {
            List<string[]> tokensList = sent.TokensList;
            if (tokensList.Count != States.Length)
            {
                throw new DataMisalignedException(String.Format("Error: Inconsistent token({0}) and state({1}) size. Tokens list: {2}",
                    tokensList.Count, States.Length, sent.ToString()));
            }

            for (int i = 0; i < tokensList.Count; i++)
            {
                string strTagName = tokensList[i][tokensList[i].Length - 1];
                int tagId = tagSet.GetIndex(strTagName);
                if (tagId < 0)
                {
                    throw new DataMisalignedException(String.Format("Error: tag {0} is unknown. Tokens list: {1}", 
                        strTagName, sent.ToString()));
                }

                States[i].Label = tagId;
            }
        }

        public Sequence(int numStates)
        {
            States = new State[numStates];
            for (int i = 0; i < numStates; i++)
            {
                States[i] = new State();
            }
        }

    }
}
