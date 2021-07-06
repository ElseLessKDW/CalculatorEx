using System;
using System.ComponentModel;
using System.Windows.Input;

namespace WpfApp14
{
    // 메인윈도우에 DataBinding 시킬 속성을들 갖고 있음
    public class CalcViewModel : INotifyPropertyChanged //-> 속성 값이 바뀌면 동작하는 인터페이스
    {
        // 입력 값에 대응될 text
        string inputString = "";
        // 결과 값에 대응될 text
        string displayText = "";

        //View와 바인딩된 속성값이 바뀔때 이를 WPF에게 알리기 위한 이벤트
        public event PropertyChangedEventHandler PropertyChanged;

        // 생성자, 명령객체들을 초기화
        // 명령객체들은 UI쪽 버튼의 Command에 바인딩 되어 있다.   
        public CalcViewModel()
        {
            //이벤트 핸들러 정의
            //숫자 버튼을 클릭할 때 실행
            this.Append = new Append(this);

            //백스페이스 버튼을 클릭할 때 실행, 한글자 삭제
            this.Backspace = new Backspace(this);

            //출력화면 클리어
            this.Clear = new Clear(this);

            //+, -, *, / 등 연산자 클릭할 때 실행
            this.Operator = new Operator(this);

            // ‘=’ 버튼을 클릭할 때 실행
            this.Calculate = new Calculate(this);
        }

        // 속성이 변동 될 때 동작할 메서드
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // 계산기의 입력 값과 바인딩 된 속성
        public string InputString
        {
            internal set // set 메서드
            {
                if (inputString != value) // 입력값과 기록값이 다른 경우
                {
                    inputString = value; // 새로운 입력값을 넣어주고
                    OnPropertyChanged("InputString"); // 호출 시점 : InputString의 Property 변동 시
                    if (value != "")
                    {
                        // 숫자를 여러개 입력하면 계속 화면에 출력하기 위해
                        DisplayText = value;
                    }
                }
            }
            get { return inputString; } // get 메서드
        }

        //계산기의 출력창과 바인딩된 속성
        public string DisplayText
        {
            internal set // set 메서드
            {
                if (displayText != value)
                {
                    displayText = value;
                    OnPropertyChanged("DisplayText"); // 호출 시점 : DisplayText의 property 변동 시
                }
            }
            get { return displayText; } // get 메서드
        }

        public string Op { get; set; } // Opertaor
        public double? Op1 { get; set; } // Operand 1

        // Command Objet
        public ICommand Append { protected set; get; }
        public ICommand Backspace { protected set; get; }
        public ICommand Clear { protected set; get; }
        public ICommand Operator { protected set; get; }
        public ICommand Calculate { protected set; get; }


    }
}