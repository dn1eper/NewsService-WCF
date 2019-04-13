using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Server
{
    [DataContract]
    public class Article
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public List<string> Categories { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public DateTime Date { get; set; }

        public bool IsCategory(string category)
        {
            foreach (string item in Categories)
            {
                if (item.Contains(category))
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return Title;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return Link.GetHashCode();
        }
    }
}
