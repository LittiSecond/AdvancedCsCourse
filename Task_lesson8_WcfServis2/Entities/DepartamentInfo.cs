using System.Runtime.Serialization;
using System.ServiceModel;

namespace Task_lesson8_WcfService
{
    [DataContract]
    public class DepartamentInfo
    {
        // значение id = -1 признак ошибки, такого Departament нет.
        [DataMember]
        public int Id;
        [DataMember]
        public string Name;
        [DataMember]
        public string Info;
        [DataMember]
        public int ChiefId;

        public static DepartamentInfo Error
        {
            get
            {
                return new DepartamentInfo
                {
                    Id = -1,
                    Name = null,
                    Info = null,
                    ChiefId = -1
                };
            }
        }
    }
}
