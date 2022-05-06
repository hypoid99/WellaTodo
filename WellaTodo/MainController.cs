// ----------------------------------------------------------
// Main Controller
// ----------------------------------------------------------
// 1. 사용자 요청을 분석한다
// 2. 뷰를 통해 입력된 데이터 가져오기
// 3. 프레임(뷰) 이동
// 4. 유효성 검사
// 5. 모델 객체 생성
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace WellaTodo
{
	public enum ControllerResult
	{
		CM_OK,
		CM_TEXT_LENGTH_ZERO,
		CM_RESERVED_MENU,
		CM_SAME_TEXT_EXIST,
		CM_ERROR
	}

	public class MainController : IController
	{
		IView m_view;
		List<IView> m_viewList = new List<IView>();
		MainModel m_model;

		// --------------------------------------------------
		// Constructor
		// --------------------------------------------------
		public MainController(MainModel m)
        {
			m_model = m;
		}

		public MainController(IView v, IModel m)
		{
			m_view = v;
			m_model = (MainModel)m;
		}

		public MainController(IView v, MainModel m)
		{
			m_view = v;
			m_model = m;
		}

		// ----------------------------------------------------
		// View Event
		// ----------------------------------------------------
		public void Add_View(IView view)
        {
			view.SetController(this);
			view.View_Changed_Event += new ViewHandler<IView>(View_Changed_Event_method);

			m_model.Add_Observer((IModelObserver)view);
        }

		public void View_Changed_Event_method(IView v, ViewEventArgs e)
		{
			Send_Log_Message(e.Msg);
		}

		public void Send_Log_Message(string msg)
        {
			m_model.Notify_Log_Message(msg);
        }

		public void Verify_DataCell(CDataCell dc)
        {
			m_model.Verify_DataCell(dc);
        }

		// --------------------------------------------------------
		// Load/Save/Open/Print 메서드
		// --------------------------------------------------------
		public void Load_Data_File()
        {
			Send_Log_Message("2>MainController::Load_Data_File");
			m_model.Load_Data();
		}

		public void Save_Data_File()
        {
			Send_Log_Message("2>MainController::Save_Data_File");
			m_model.Save_Data();
		}

		public void Print_Data_File()
        {
			Send_Log_Message("2>MainController::Print_Data_File");
			m_model.Print_Data();
        }

		public void New_Data_File()
        {
			Send_Log_Message("2>MainController::New_Data_File");
		}

		public void Open_Data_File(string filePath)
        {
			Send_Log_Message("2>MainController::Open_Data_File" + filePath);
			m_model.Open_Data(filePath);	
		}

		// -----------------------------------------------------------
		// Perform Command (Menulist) - Add/Delete/Rename/Up/Down
		// -----------------------------------------------------------
		public ControllerResult Perform_Menulist_Add(string MenuName)
        {
			if (MenuName.Length == 0)
			{
				Send_Log_Message("Warning>MainController::Perform_Menulist_Add -> Leghth of Menu Name is zero!!");
				return ControllerResult.CM_TEXT_LENGTH_ZERO;
			}

			if (MenuName == "오늘 할 일" || MenuName == "중요" || MenuName == "계획된 일정" || MenuName == "완료됨" || MenuName == "작업")
			{
				Send_Log_Message("Warning>MainController::Perform_Menulist_Add -> Can't Add MenuList for Reserved Menu!!");
				return ControllerResult.CM_RESERVED_MENU;
			}

			if (m_model.IsThereSameMenuName(MenuName))
            {
				Send_Log_Message("Warning>MainController::Perform_Menulist_Add -> Can't Add MenuList for Same menu name exist!!");
				return ControllerResult.CM_SAME_TEXT_EXIST;
			}

			Send_Log_Message("2>MainController::Perform_Menulist_Add : " + MenuName);
			m_model.Menulist_Add(MenuName);
			return ControllerResult.CM_OK;
		}

		public ControllerResult Perform_Menulist_Rename(string source, string target)
        {
			if (target == source)
			{
				Send_Log_Message("Warning>MainController::Perform_Menulist_Rename -> Before & After Name is same!!");
				return ControllerResult.CM_SAME_TEXT_EXIST;
			}

			if (target.Length == 0)
			{
				Send_Log_Message("Warning>MainController::Perform_Menulist_Rename -> Leghth of Menu Name is zero!!");
				return ControllerResult.CM_TEXT_LENGTH_ZERO;
			}

			if (target == "오늘 할 일" || target == "중요" || target == "계획된 일정" || target == "완료됨" || target == "작업")
			{
				Send_Log_Message("Warning>MainController::Perform_Menulist_Rename -> Can't Add MenuList for Reserved Menu!!");
				return ControllerResult.CM_RESERVED_MENU;
			}

			if (m_model.IsThereSameMenuName(target))
			{
				Send_Log_Message("Warning>MainController::Perform_Menulist_Rename -> Can't Add MenuList for Same menu name exist!!");
				return ControllerResult.CM_SAME_TEXT_EXIST;
			}

			Send_Log_Message("2>MainController::Perform_Menulist_Rename : from " + source + " to " + target);
			m_model.Menulist_Rename(source, target);
			return ControllerResult.CM_OK;
        }
		
		public ControllerResult Perform_Menulist_Delete(string target)
        {
			Send_Log_Message("2>MainController::Perform_Menulist_Delete : " + target);
			m_model.Menulist_Delete(target);
			return ControllerResult.CM_OK;
		}

		public ControllerResult Perform_Menulist_Up(string target)
		{
			Send_Log_Message("2>MainController::Perform_Menulist_Up : " + target);
			m_model.Menulist_Up(target);
			return ControllerResult.CM_OK;
		}

		public ControllerResult Perform_Menulist_Down(string target)
        {
			Send_Log_Message("2>MainController::Perform_Menulist_Down : " + target);
			m_model.Menulist_Down(target);
			return ControllerResult.CM_OK;
		}

		public ControllerResult Perform_Munulist_MoveTo(string source, string target)
		{
			if (source == target)
			{
				Send_Log_Message("2>MainController::Perform_Munulist_MoveTo -> Same task can't move");
				return ControllerResult.CM_SAME_TEXT_EXIST;
			}

			if (source == "작업" || target == "작업")
			{
				Send_Log_Message("2>MainController::Perform_Munulist_MoveTo -> Can't move 작업 Task");
				return ControllerResult.CM_RESERVED_MENU;
			}

			Send_Log_Message("2>MainController::Perform_Munulist_MoveTo -> Source : " + source + " Target : " + target);
			m_model.Menulist_MoveTo(source, target);

			return ControllerResult.CM_OK;
		}

		// -----------------------------------------------------------
		// Perform Command (Task)
		// -----------------------------------------------------------
		public ActionResult Perform_Add_Task_From_MyToday(string title)
        {
			if (!Check_Input_String(title))
			{
				Send_Log_Message("Warning>MainController::Perform_Add_Task_From_MyToday -> Check Input String");
				return ActionResult.Fail("항목 입력시 공백이나 특수문자가 포함되어 있읍니다.");
			}

			CDataCell dc = new CDataCell();
			DateTime dt = DateTime.Now;

			dc.DC_listName = "작업";
			dc.DC_title = title;
			dc.DC_myToday = true;
			dt = dt.AddDays(1);
			dc.DC_myTodayTime = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);

			Send_Log_Message("2>MainController::Perform_Add_Task_From_MyToday : " + dc.DC_title);
			m_model.Add_Task(dc);

			return ActionResult.Ok();
		}

		public ActionResult Perform_Add_Task_From_Important(string title)
		{
			if (!Check_Input_String(title))
			{
				Send_Log_Message("Warning>MainController::Perform_Add_Task_From_Important -> Check Input String");
				return ActionResult.Fail("항목 입력시 공백이나 특수문자가 포함되어 있읍니다.");
			}

			CDataCell dc = new CDataCell();

			dc.DC_listName = "작업";
			dc.DC_title = title;
			dc.DC_important = true;

			Send_Log_Message("2>MainController::Perform_Add_Task_From_Important : " + dc.DC_title);
			m_model.Add_Task(dc);

			return ActionResult.Ok();
		}

		public ActionResult Perform_Add_Task_From_Planned(string title)
		{
			if (!Check_Input_String(title))
			{
				Send_Log_Message("Warning>MainController::Perform_Add_Task_From_Planned -> Check Input String");
				return ActionResult.Fail("항목 입력시 공백이나 특수문자가 포함되어 있읍니다.");
			}

			CDataCell dc = new CDataCell();
			DateTime dt = DateTime.Now;

			dc.DC_listName = "작업";
			dc.DC_title = title;
			dc.DC_deadlineType = 1;
			dt = dt.AddDays(1);
			dc.DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);

			Send_Log_Message("2>MainController::Perform_Add_Task_From_Planned : " + dc.DC_title);
			m_model.Add_Task(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Add_Task_From_List(string listName, string title)
		{
			if (!Check_Input_String(title))
			{
				Send_Log_Message("Warning>MainController::Perform_Add_Task_From_List -> Check Input String");
				return ActionResult.Fail("항목 입력시 공백이나 특수문자가 포함되어 있읍니다.");
			}

			CDataCell dc = new CDataCell();

			dc.DC_listName = listName;
			dc.DC_title = title;

			Send_Log_Message("2>MainController::Perform_Add_Task_From_Planned : " + dc.DC_title);
			m_model.Add_Task(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Delete_Task(CDataCell dc)
        {
			Send_Log_Message("2>MainController::Perform_Delete_Task : [" + dc.DC_task_ID + "]" + dc.DC_title);
			m_model.Delete_Task(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Modify_MyToday(CDataCell dc)
        {
			DateTime dt = DateTime.Now;
			if (dc.DC_myToday)
			{
				dc.DC_myToday = false;  // 해제
				dc.DC_myTodayTime = default;
			}
			else
			{
				dc.DC_myToday = true; // 설정
				dc.DC_myTodayTime = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);
			}

			Send_Log_Message("2>MainController::Perform_MyToday_Process : " + dc.DC_title);
			m_model.Modifiy_MyToday(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Remind_Today(CDataCell dc)
        {
			DateTime dt = DateTime.Now;
            dt = dt.Minute < 30 ? dt.AddHours(3) : dt.AddHours(4);
            dc.DC_remindType = 1;
            dc.DC_remindTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 00, 00);

			Send_Log_Message("2>MainController::Perform_Remind_Today : " + dc.DC_title);
			m_model.Modifiy_Remind(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Remind_Tomorrow(CDataCell dc)
		{
			DateTime dt = DateTime.Now;
			dt = dt.AddDays(1);
			dc.DC_remindType = 2;
			dc.DC_remindTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 00, 00);

			Send_Log_Message("2>MainController::Perform_Remind_Tomorrow : " + dc.DC_title);
			m_model.Modifiy_Remind(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Remind_NextWeek(CDataCell dc)
		{
			DateTime dt = DateTime.Now;
			dt = dt.AddDays(8 - (int)dt.DayOfWeek);
			dc.DC_remindType = 3;
			dc.DC_remindTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 00, 00);

			Send_Log_Message("2>MainController::Perform_Remind_NextWeek : " + dc.DC_title);
			m_model.Modifiy_Remind(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Remind_Select(CDataCell dc, DateTime dt)
		{
			dc.DC_remindType = 4;
			dc.DC_remindTime = dt;

			Send_Log_Message("2>MainController::Perform_Remind_Select : " + dc.DC_title);
			m_model.Modifiy_Remind(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Remind_Delete(CDataCell dc)
		{
			dc.DC_remindType = 0;
			dc.DC_remindTime = default;

			Send_Log_Message("2>MainController::Perform_Remind_Select : " + dc.DC_title);
			m_model.Modifiy_Remind(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Planned_Today(CDataCell dc)
		{
			DateTime dt = DateTime.Now;
			dc.DC_deadlineType = 1;
			dc.DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);

			Send_Log_Message("2>MainController::Perform_Planned_Today : " + dc.DC_title);
			m_model.Modifiy_Planned(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Planned_Tomorrow(CDataCell dc)
		{
			DateTime dt = DateTime.Now;
			dt = dt.AddDays(1);
			dc.DC_deadlineType = 2;
			dc.DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);

			Send_Log_Message("2>MainController::Perform_Planned_Tomorrow : " + dc.DC_title);
			m_model.Modifiy_Planned(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Planned_NextWeek(CDataCell dc)
		{
			DateTime dt = DateTime.Now;
			dt = dt.AddDays(8 - (int)dt.DayOfWeek);
			dc.DC_deadlineType = 3;
			dc.DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);

			Send_Log_Message("2>MainController::Perform_Planned_NextWeek : " + dc.DC_title);
			m_model.Modifiy_Planned(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Planned_Select(CDataCell dc, DateTime dt)
		{
			if (dt.Hour == 0 && dt.Minute == 0 && dt.Second == 0) // 시간을 입력하지 않을때
			{
				dt = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);
			}

			dc.DC_deadlineType = 4;
			dc.DC_deadlineTime = dt;

			Send_Log_Message("2>MainController::Perform_Planned_Select : " + dc.DC_title);
			m_model.Modifiy_Planned(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Planned_Delete(CDataCell dc)
		{
			dc.DC_deadlineType = 0;
			dc.DC_deadlineTime = default;

			Send_Log_Message("2>MainController::Perform_Planned_Delete : " + dc.DC_title);
			m_model.Modifiy_Planned(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Modify_Repeat(CDataCell dc, int type, DateTime dt)
		{
			dc.DC_repeatType = type;
			dc.DC_repeatTime = dt;

			Send_Log_Message("2>MainController::Perform_Modify_Repeat : type [" + type + "]" + dc.DC_title);
			m_model.Modifiy_Repeat(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Repeat_EveryDay(CDataCell dc)
		{
			DateTime dt = DateTime.Now;
			dt = dt.AddDays(1);
			dt = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
			dc.DC_repeatType = 1;
			dc.DC_repeatTime = dt;

			Send_Log_Message("2>MainController::Perform_Repeat_EveryDay : " + dc.DC_title);
			m_model.Modifiy_Repeat(dc);

			if (dc.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때 오늘까지
			{
				Perform_Planned_Today(dc);
			}
			return ActionResult.Ok();
		}

		public ActionResult Perform_Repeat_WorkingDay(CDataCell dc)
		{
			DateTime dt = DateTime.Now;
			DayOfWeek dw = dt.DayOfWeek;
			switch (dw)
			{
				case DayOfWeek.Monday:
					dt = dt.AddDays(1);
					break;
				case DayOfWeek.Tuesday:
					dt = dt.AddDays(1);
					break;
				case DayOfWeek.Wednesday:
					dt = dt.AddDays(1);
					break;
				case DayOfWeek.Thursday:
					dt = dt.AddDays(1);
					break;
				case DayOfWeek.Friday:
					dt = dt.AddDays(3);
					break;
				case DayOfWeek.Saturday:
					dt = dt.AddDays(2);
					break;
				case DayOfWeek.Sunday:
					dt = dt.AddDays(1);
					break;
			}
			dt = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);

			dc.DC_repeatType = 2;
			dc.DC_repeatTime = dt;

			Send_Log_Message("2>MainController::Perform_Repeat_WorkingDay : " + dc.DC_title);
			m_model.Modifiy_Repeat(dc);

			if (dc.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때 오늘까지
			{
				Perform_Planned_Today(dc);
			}
			return ActionResult.Ok();
		}

		public ActionResult Perform_Repeat_EveryWeek(CDataCell dc)
		{
			DateTime dt = DateTime.Now;
			dt = dt.AddDays(7);
			dt = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
			dc.DC_repeatType = 3;
			dc.DC_repeatTime = dt;

			Send_Log_Message("2>MainController::Perform_Repeat_EveryWeek : " + dc.DC_title);
			m_model.Modifiy_Repeat(dc);

			if (dc.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때 오늘까지
			{
				Perform_Planned_Today(dc);
			}
			return ActionResult.Ok();
		}

		public ActionResult Perform_Repeat_EveryMonth(CDataCell dc)
		{
			DateTime dt = DateTime.Now;
			dt = dt.AddMonths(1); // 매달 말일 계산 필요 - 28/29/30/31일 경우
			dt = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
			dc.DC_repeatType = 4;
			dc.DC_repeatTime = dt;

			Send_Log_Message("2>MainController::Perform_Repeat_EveryMonth : " + dc.DC_title);
			m_model.Modifiy_Repeat(dc);

			if (dc.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때 오늘까지
			{
				Perform_Planned_Today(dc);
			}
			return ActionResult.Ok();
		}

		public ActionResult Perform_Repeat_EveryYear(CDataCell dc)
		{
			DateTime dt = DateTime.Now;
			dt = dt.AddYears(1);  // 윤년 계산 필요 2월29일
			dt = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
			dc.DC_repeatType = 5;
			dc.DC_repeatTime = dt;

			Send_Log_Message("2>MainController::Perform_Repeat_EveryYear : " + dc.DC_title);
			m_model.Modifiy_Repeat(dc);

			if (dc.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때 오늘까지
			{
				Perform_Planned_Today(dc);
			}
			return ActionResult.Ok();
		}

		public ActionResult Perform_Repeat_Delete(CDataCell dc)
		{
			dc.DC_repeatType = 0;
			dc.DC_repeatTime = default;

			Send_Log_Message("2>MainController::Perform_Repeat_Delete : " + dc.DC_title);
			m_model.Modifiy_Repeat(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Complete_Process(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Complete_Process : " + dc.DC_complete);
			m_model.Complete_Process(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Important_Process(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Important_Process : " + dc.DC_important);
			m_model.Important_Process(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Modify_Task_Title(CDataCell dc, string title) // MainForm에서 실행됨
		{
			// 입력 사항에 오류 및 특수문자("&")가 있는지 체크할 것
			if (title.Length == 0)
			{
				Send_Log_Message("Warning>MainController::Perform_Modify_Task_Title -> Leghth of Title is zero!!");
				return ActionResult.Fail(ErrorCode.E_TEXT_IS_EMPTY, "입력한 제목이 공백이기 때문에 항목의 제목을 수정할 수 없읍니다.");
			}

			if (dc.DC_title == title)
            {
				Send_Log_Message("Warning>MainController::Perform_Modify_Task_Title -> Title is same!!");
				return ActionResult.Fail("입력한 제목이 기존 제목과 동일하여 수정할 수 없읍니다.");
			}

			dc.DC_title = title;

			Send_Log_Message("2>MainController::Perform_Modify_Task_Title : " + dc.DC_title);
			m_model.Modify_Task_Title(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Modify_Task_Title(CDataCell dc) // CalendarForm에서 실행됨
		{
			// 입력 사항에 오류 및 특수문자("&")가 있는지 체크할 것
			if (dc.DC_title.Length == 0)
			{
				Send_Log_Message("Warning>MainController::Perform_Modify_Task_Title -> Leghth of Title is zero!!");
				return ActionResult.Fail("입력한 제목이 공백이기 때문에 항목의 제목을 수정할 수 없읍니다.");
			}

			Send_Log_Message("2>MainController::Perform_Modify_Task_Title : " + dc.DC_title);
			m_model.Modify_Task_Title(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Modify_Task_Memo(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Modify_Task_Memo : " + dc.DC_title);
			m_model.Modify_Task_Memo(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Task_Move_To(CDataCell source, CDataCell target)
        {
			if (source.DC_task_ID == target.DC_task_ID)
			{
				Send_Log_Message("2>MainController::Perform_Task_Move_To -> Same task can't move");
				return ActionResult.Fail("대상 항목을 동일할 경우에는 이동할 수 없읍니다.");
			}

			if (source.DC_complete || target.DC_complete)
			{
				Send_Log_Message("2>MainController::Perform_Task_Move_To -> Can't move Completed Task");
				return ActionResult.Fail("완료된 항목은 이동할 수 없읍니다.");
			}

			Send_Log_Message("2>MainController::Perform_Task_Move_To -> Source : " + source.DC_title + " Target : " + target.DC_title);
			m_model.Task_Move_To(source, target);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Task_Move_Up(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Task_Move_Up : " + dc.DC_title);
			m_model.Task_Move_Up(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Task_Move_Down(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Task_Move_Down : " + dc.DC_title);
			m_model.Task_Move_Down(dc);
			return ActionResult.Ok();
		}

		public ActionResult Perform_Transfer_Task(CDataCell dc, string target)
        {
			if (dc.DC_listName == target)
			{
				Send_Log_Message("Warning>MainController::Perform_Transfer_Task -> Can't transfer item as same list");
				return ActionResult.Fail("대상 항목을 동일한 목록으로 이동할 수 없읍니다.");
			}

			Send_Log_Message("2>MainController::Perform_Trasnfer_Task : from " + dc.DC_listName + " to " + target);
			m_model.Transfer_Task(dc, target);
			return ActionResult.Ok();
        }

		// ----------------------------------------------------------
		// Perform Command - NotePad 문서편집
		// ----------------------------------------------------------
		public void Perform_Add_Note(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Add_Note : " + dc.DC_title);
			m_model.Add_Note(dc);
		}

		public void Perform_Modify_Note(CDataCell dc)
        {
			Send_Log_Message("2>MainController::Perform_Modify_Note_Text : " + dc.DC_title);
			m_model.Modify_Note(dc);
		}

		public void Perform_Delete_Note(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Delete_Note : " + dc.DC_title);
			m_model.Delete_Note(dc);
		}

		public void Perform_Duplicate_Note(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Duplicate_Note : " + dc.DC_title);
			m_model.Duplicate_Note(dc);
		}

		public bool Perform_Rename_Note(CDataCell dc, string renameText)
		{
			if (renameText == dc.DC_title)
			{
				Send_Log_Message("Warning>MainController::Perform_Rename_Note -> Before & After Name is same!!");
				return false;
			}

			if (renameText.Length == 0)
			{
				Send_Log_Message("Warning>MainController::Perform_Rename_Note -> Leghth of Menu Name is zero!!");
				return false;
			}

			Send_Log_Message("2>MainController::Perform_Rename_Note : from " + dc.DC_title + " to " + renameText);
			m_model.Rename_Note(dc, renameText);
			return true;
		}

		public void Perform_MoveUp_Note(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_MoveUp_Note : " + dc.DC_title);
			m_model.MoveUp_Note(dc);
		}

		public void Perform_MoveDown_Note(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_MoveDown_Note : " + dc.DC_title);
			m_model.MoveDown_Note(dc);
		}

		// ----------------------------------------------------------
		// Perform Command - Calendar 일정 
		// ----------------------------------------------------------
		public void Perform_Add_Plan(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Add_Plan : " + dc.DC_title);
			m_model.Add_Plan(dc);
		}

		// ----------------------------------------------------------
		// Perform Command - BulletinBoard 메모
		// ----------------------------------------------------------
		public void Perform_Add_Memo(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Add_Memo : " + dc.DC_title);
			m_model.Add_Memo(dc);
		}

		public void Perform_Delete_Memo(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Delete_Memo : [" + dc.DC_task_ID + "]" + dc.DC_title);
			m_model.Delete_Memo(dc);
		}

		public void Perform_Modify_Memo_Text(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Modify_Memo_Text : " + dc.DC_title);
			m_model.Modify_Memo_Text(dc);
		}

		public bool Perform_Modify_Memo_Title(CDataCell dc, string title)
		{
			// 입력 사항에 오류 및 특수문자("&")가 있는지 체크할 것
			if (title.Length == 0)
			{
				Send_Log_Message("Warning>MainController::Perform_Modify_Memo_Title -> Leghth of Title is zero!!");
				return false;
			}

			if (dc.DC_title == title)
			{
				Send_Log_Message("Warning>MainController::Perform_Modify_Memo_Title -> Title is same!!");
				return true;
			}

			dc.DC_title = title;

			Send_Log_Message("2>MainController::Perform_Modify_Memo_Title : " + dc.DC_title);
			m_model.Modify_Memo_Title(dc);
			return true;
		}

		public void Perform_Modify_Memo_Archive(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Modify_Memo_Archive : " + dc.DC_title);
			m_model.Modify_Memo_Archive(dc);
		}

		public void Perform_Modify_Memo_Color(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Modify_Memo_Color : " + dc.DC_title);
			m_model.Modify_Memo_Color(dc);
		}

		public void Perform_Modify_Memo_Tag(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Modify_Memo_Tag : " + dc.DC_title);
			m_model.Modify_Memo_Tag(dc);
		}

		public void Perform_Modify_Memo_Alarm(CDataCell dc, DateTime dt)
		{
			if (dt == default)
            {
				dc.DC_remindType = 0;
				dc.DC_remindTime = dt;
			}
            else
            {
				dc.DC_remindType = 4;
				dc.DC_remindTime = dt;
			}

			Send_Log_Message("2>MainController::Perform_Remind_Select : " + dc.DC_title);
			m_model.Modifiy_Memo_Alarm(dc);
		}

		public void Perform_Modify_Memo_Schedule(CDataCell dc, DateTime dt)
		{
			if (dt == default)
			{
				dc.DC_deadlineType = 0;
				dc.DC_deadlineTime = dt;
			}
			else
			{
				if (dt.Hour == 0 && dt.Minute == 0 && dt.Second == 0) // 시간을 입력하지 않을때
				{
					dt = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);
				}

				dc.DC_deadlineType = 4;
				dc.DC_deadlineTime = dt;
			}

			Send_Log_Message("2>MainController::Perform_Modify_Memo_Schedule : " + dc.DC_title);
			m_model.Modifiy_Memo_Schedule(dc);
		}

		public void Perform_Memo_Move_To(CDataCell source, CDataCell target)
		{
			Send_Log_Message("2>MainController::Perform_Memo_Move_To -> Source : " + source.DC_title + " Target : " + target.DC_title);
			m_model.Memo_Move_To(source, target);
		}

		// -----------------------------------------------------------
		// DB Query
		// -----------------------------------------------------------
		public List<string> Query_ListName()
		{
			List<string> list = new List<string>();
			IEnumerable<string> dataset = from string str in m_model.GetListCollection()
											 where true
											 select str;
			if (dataset.Count() > 0)
            {
				foreach (string item in dataset)
				{
					list.Add(item);
				}
            }
            else
            {
				Console.WriteLine("dataset.Count() is 0");
            }

			//Send_Log_Message("2>MainController::Query_ListName");
			return list;
		}

		public IEnumerable<CDataCell> Query_MyToday()
        {
			IEnumerable<CDataCell> dataset = from CDataCell dt in m_model.GetTaskCollection() 
											 where dt.DC_myToday && !dt.DC_complete select dt;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_MyToday -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Important()
		{
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetTaskCollection() 
											 where dc.DC_important && !dc.DC_complete select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_Important -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Planned()
		{
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetTaskCollection() 
											 where (dc.DC_deadlineType > 0 || dc.DC_repeatType > 0) && !dc.DC_complete select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_Planned -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Complete()
		{
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetTaskCollection() 
											 where dc.DC_complete == true select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_Complete -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Task(string listname)
		{
			IEnumerable <CDataCell> dataset = from CDataCell dc in m_model.GetTaskCollection() 
												where dc.DC_listName == listname select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_Task -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Month_Calendar(DateTime curDate)
        {
			IEnumerable<CDataCell> dataset = from CDataCell dc
											 in m_model.GetTaskCollection()
											 where dc.DC_deadlineType > 0
											 && (curDate.Date == dc.DC_deadlineTime.Date)
											 select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_Month_Calendar -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Task_Calendar(CDataCell sd)
        {
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetTaskCollection()
											 where dc.DC_task_ID == sd.DC_task_ID
											 select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_Task_Calendar -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_All_Task()
        {
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetTaskCollection()
											 where true
											 select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_All_Task -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_NoteFile()
		{
			IEnumerable<CDataCell> dataset = from CDataCell dt in m_model.GetNoteCollection()
											 where true
											 select dt;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_BulletineBoard()
		{
			IEnumerable<CDataCell> dataset = from CDataCell dt in m_model.GetMemoCollection()
											 where dt.DC_bulletin && (!dt.DC_archive)
											 select dt;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_BulletineBoard -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_BulletineBoard_Archive()
		{
			IEnumerable<CDataCell> dataset = from CDataCell dt in m_model.GetMemoCollection()
											 where dt.DC_bulletin && (dt.DC_archive)
											 select dt;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_BulletineBoard -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_BulletineBoard_Tag(int tag)
        {
			IEnumerable<CDataCell> dataset = from CDataCell dt in m_model.GetMemoCollection()
											 where dt.DC_bulletin && (!dt.DC_archive) && dt.DC_memoTag == tag
											 select dt;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_BulletineBoard_Tag -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		// ----------------------------------------------------
		// List Deep Copy
		// ----------------------------------------------------
		private List<CDataCell> List_DeepCopy(IEnumerable<CDataCell> dataset)
        {
			List<CDataCell> deepCopy = new List<CDataCell>();
			foreach (CDataCell dc in dataset)
			{
				//deepCopy.Add((CDataCell)dc.Clone());
				//deepCopy.Add((CDataCell)DeepClone(dc));
				deepCopy.Add((CDataCell)SerializableDeepClone(dc));
			}
			return deepCopy;
		}

		// ----------------------------------------------------
		// 메서드
		// ----------------------------------------------------
		private bool Check_Input_String(string text)
        {
			// 입력 사항에 오류 및 특수문자("&")가 있는지 체크할 것
			if (text.Length == 0)
			{
				Send_Log_Message("Warning>MainController::Check_Input_String -> Leghth of Text is zero!!");
				return false;
			}

			if (text.IndexOf("&") > -1)
			{
				Send_Log_Message("Warning>MainController::Check_Input_String -> 특수문자 포함");
				return false;
			}

			return true;
		}

		// ----------------------------------------------------
		// Serializable 객체에 대한  Deep Clone 구현
		// ----------------------------------------------------
		private static T SerializableDeepClone<T>(T obj)
		{
			using (var ms = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(ms, obj);
				ms.Position = 0;

				return (T)formatter.Deserialize(ms);
			}
		}

		// ----------------------------------------------------
		// Deep Clone 구현
		// ----------------------------------------------------
		private static T DeepClone<T>(T obj)
		{
			if (obj == null)
				throw new ArgumentNullException("Object cannot be null.");

			return (T)Process(obj, new Dictionary<object, object>() { });
		}

		private static object Process(object obj, Dictionary<object, object> circular)
		{
			if (obj == null) return null;

			Type type = obj.GetType();

			if (type.IsValueType || type == typeof(string)) return obj;

			if (type.IsArray)
			{
				if (circular.ContainsKey(obj)) return circular[obj];

				string typeNoArray = type.FullName.Replace("[]", string.Empty);
				Type elementType = Type.GetType(typeNoArray + ", " + type.Assembly.FullName);
				var array = obj as Array;
				Array arrCopied = Array.CreateInstance(elementType, array.Length);

				circular[obj] = arrCopied;

				for (int i = 0; i < array.Length; i++)
				{
					object element = array.GetValue(i);
					object objCopy = null;

					if (element != null && circular.ContainsKey(element))
						objCopy = circular[element];
					else
						objCopy = Process(element, circular);

					arrCopied.SetValue(objCopy, i);
				}
				return Convert.ChangeType(arrCopied, obj.GetType());
			}

			if (type.IsClass)
			{
				if (circular.ContainsKey(obj)) return circular[obj];

				object objValue = Activator.CreateInstance(obj.GetType());
				circular[obj] = objValue;
				FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

				foreach (FieldInfo field in fields)
				{
					object fieldValue = field.GetValue(obj);

					if (fieldValue == null)
						continue;

					object objCopy = circular.ContainsKey(fieldValue) ? circular[fieldValue] : Process(fieldValue, circular);
					field.SetValue(objValue, objCopy);
				}
				return objValue;
			}
			else
			{
				throw new ArgumentException("Unknown type");
			}
		}
	}
}


