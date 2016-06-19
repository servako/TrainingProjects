using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.ComponentModel;

namespace CallerInfoAttbLib
{
    public class Employee:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Employee([CallerMemberName]string sourceMemberName = "", [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNo = 0)
        {
            Debug.WriteLine("Member Name : " + sourceMemberName);
            Debug.WriteLine("File Name : " + sourceFilePath);
            Debug.WriteLine("Line No. : " + sourceLineNo);
        }

        private int intEmployeeID;
        public int EmployeeID
        {
            get
            {
                return intEmployeeID;
            }
            set
            {
                SetPropertyValue<int>(ref intEmployeeID, value);
            }
        }

        private string strFirstName;
        public string FirstName
        {
            get 
            {
                return strFirstName;
            }
            set 
            {
                SetPropertyValue<string>(ref strFirstName, value);
            }
        }

        private string strLastName;
        public string LastName
        {
            get
            {
                return strLastName;
            }
            set
            {
                SetPropertyValue<string>(ref strLastName, value);
            }
        }

        protected bool SetPropertyValue<T>(ref T varName, T propValue, [CallerMemberName] string propName = null)
        {
            varName = propValue;
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs evt = new PropertyChangedEventArgs(propName);
                this.PropertyChanged(this, evt);
                Debug.WriteLine("Member Name : " + propName);
            }
            return true;
        }

        public string AddEmployee([CallerMemberName]string sourceMemberName="",[CallerFilePath]string sourceFilePath="",[CallerLineNumber]int sourceLineNo=0)
        {
            Debug.WriteLine("Member Name : " + sourceMemberName);
            Debug.WriteLine("File Name : " + sourceFilePath);
            Debug.WriteLine("Line No. : " + sourceLineNo);
            //do database INSERT here
            return "Employee added successfully!"; 
        }
    
    }
}
