
namespace Task_lesson6.Structures
{
    public struct DepartamentInfo
    {
        // значение id = -1 признак ошибки, такого Departament нет.

        public int id;
        public string name;
        public string info;
        public int chiefId;

        public static DepartamentInfo Error
        {
            get
            {
                return new DepartamentInfo
                {
                    id = -1,
                    name = null,
                    info = null,
                    chiefId = -1
                };
            }
        }
    }
}
