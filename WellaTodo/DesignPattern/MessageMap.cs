using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo.DesignPattern
{
    /*
class Window;
 
struct AFX_MSG
{
    int message;                // 메세지 번호
    void(Window::*handler)();    // 처리할 함수
};
 
class Window
{
    static map<int, Window*> this_map;        // MFC, QT 등에서 이 자료구조를 핸들맵 이라고 부릅니다.
    int hwnd;
public:
    void Create() { hwnd = IoMakeWindow(foo); this_map[hwnd] = this; }
 
    virtual AFX_MSG* GetMessageMap() { return 0; }
 
    static int foo(int h, int msg, int a, int b){
        Window* pThis = static_cast<Window*>(this_map[h]);
 
        if (pThis == 0)return 0;
        AFX_MSG* arr = pThis->GetMessageMap();
        if (arr == 0) return 0;
        for (; arr->message; arr++) {
            if (arr->message == msg)     {
                void(Window::*f)() = arr->handler;
                (pThis->*f)();        // 해당 메세지 함수 실행
            }
        }
        return 0;
    }
};
 
map<int, Window*> Window::this_map;
 
class MyWindow : public Window
{
public:
    void OnLButtonDown() {  cout << "MyWindow LButtonDown" << endl; }
    void OnKeyDown() { cout << "MyWindow KeyDown" << endl; }
 
    DECLARE_MESSAGE_MAP()
};
 
BEGIN_MESSAGE_MAP(MyWindow, Window)
    ADD_MESSAGE(WM_LBUTTONDOWN, &MyWindow::OnLButtonDown)
    ADD_MESSAGE(WM_KEYDOWN, &MyWindow::OnKeyDown)
END_MESSAGE_MAP()
    */

    internal class MessageMap
    {
    }
}
