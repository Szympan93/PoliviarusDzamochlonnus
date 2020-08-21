using System.Collections.Generic;

namespace Music.Dict
{
    public class MyDictionary<T>
    {
        #region Variable
        public Dictionary<T, int> I3StateTactsForI3ClipDict { get; set; }
        public Dictionary<ActualLeader, List<T>> I1Dict { get; set; }
        public Dictionary<ActualLeader, List<T>> I2Dict { get; set; }
        public Dictionary<ActualLeader, List<T>> I3Dict { get; set; }
        #endregion

        #region Non-Unity Methods
        public Dictionary<T, int> AddI3TactsListToDict(List<int> tactsList, List<T> neutral, List<T> red, List<T> blue)
        {
            var readyDict = new Dictionary<T, int>();
            var allClipsList = new List<T>();
            allClipsList.AddRange(neutral);
            allClipsList.AddRange(red);
            allClipsList.AddRange(blue);

            for (int i = 0; i < allClipsList.Count; i++)
            {
                readyDict.Add(allClipsList[i], tactsList[i]);
            }

            return readyDict;
        }

        public Dictionary<ActualLeader, List<T>> AddClipsListToDict(List<T> neutral, List<T> red, List<T> blue)
        {
            var readyDict = new Dictionary<ActualLeader, List<T>>
        {
            { ActualLeader.None, neutral },
            { ActualLeader.Red, red },
            { ActualLeader.Blue, blue }
        };
            return readyDict;
        }
        public int GetValueTactsList(T clip)
        {
            I3StateTactsForI3ClipDict.TryGetValue(clip, out int i3Tact);
            return i3Tact;
        }
        public List<T> GetValueAudioClipList(Dictionary<ActualLeader, List<T>> iDict, ActualLeader leader)
        {
            iDict.TryGetValue(leader, out List<T> clipList);
            return clipList;
        }
        #endregion
    }
}