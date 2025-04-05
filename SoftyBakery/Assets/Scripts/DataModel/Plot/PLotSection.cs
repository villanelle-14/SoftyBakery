using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataModel.Plot
{
    [Serializable]
    public class Section
    {
        public string SectionName;

        public List<ScetionChoice> Choices;

        public string EndEvent;//结束事件

        /*public Section(PlotSections sectionData)
        {
            if (sectionData.Chioces.Count > 0)
            {
                Choices = new List<ScetionChoice>();
                foreach (var chioceData in sectionData.Chioces)
                {
                    Choices.Add(new ScetionChoice
                    {
                        ChoiceTermKey = chioceData.ChioceTermKey,
                        ChoiceEvent = chioceData.ChioceEvent,
                    });
                }
            }

            TermData termData;
            if (!sectionData.IsCombine)
            {
                SectionName = sectionData.SectionName ;
                termData = LocalizationManager.GetTermData(SectionName);
                if (termData.Description == "")
                {
                    return;
                }
                DialogContent dialogContent = JsonUtility.FromJson<DialogContent>(termData.Description);
                dialogContent.DialogKey = SectionName;
                dialogContents.Add(dialogContent);
                return;
            }

            SectionName = sectionData.SectionName + '_';
            int TailIndex = 100;
            string TailStr = TailIndex.ToString();
            string termKey = SectionName + TailStr;
            termData = LocalizationManager.GetTermData(termKey);
            
            while (termData != null)
            {
                DialogContent dialogContent = JsonUtility.FromJson<DialogContent>(termData.Description);
                dialogContent.DialogKey = termKey;
                dialogContents.Add(dialogContent);
                
                TailIndex++;
                TailStr = TailIndex.ToString();
                termKey = SectionName + TailStr;
                termData = LocalizationManager.GetTermData(termKey);
            }

            EndEvent = sectionData.EndEvent;
            /*int TailIndex = 100;
            string sectionName = SectionName;
            StringBuilder stringBuilder = new StringBuilder();
            TermData termData;

            do
            {
                stringBuilder.Clear();
                stringBuilder.Append(sectionName);
                stringBuilder.Append(TailIndex.ToString());
                string termKey = stringBuilder.ToString();
                termData = LocalizationManager.GetTermData(termKey);
                TailIndex++;
            } while (termData!= null);#1#

            DebugTermKeys();
        }*/
        
        public List<DialogContent> dialogContents = new List<DialogContent>();

        void DebugTermKeys()
        {
            string str = SectionName;
            foreach (var Key in dialogContents)
            {
                str += "\n" + Key;
            }
            Debug.Log($"str");
        }
    }

    [Serializable]
    public struct ScetionChoice
    {
        public string ChoiceTermKey;
        public string ChoiceEvent;
    }
}
