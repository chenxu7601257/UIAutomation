using CX.UIAutomation.Enum;
using System;
using System.Windows.Automation;

namespace CX.UIAutomation
{ 
    public class UIAutomationClass 
    {
        public AutomationElement Desktop;
         
        public UIAutomationClass()
        {
            //获取当前桌面的根 System.Windows.Automation.AutomationElement。
            Desktop = AutomationElement.RootElement;
        }
        
        /// <summary>
        /// 获取当前桌面下的所有子windows
        /// </summary>
        /// <returns></returns>
        public String[] GetChildrenWindowList()  
        {
            Int32 count = 0;
            AutomationElementCollection childrenAutoElements = Desktop.FindAll(TreeScope.Children,
                new PropertyCondition(AutomationElement.IsContentElementProperty,
                true));
            String[] WindowNames = new String[childrenAutoElements.Count];
            foreach (AutomationElement aeWin in childrenAutoElements)
            {
                WindowNames[count++] = aeWin.Current.Name.ToString();
            }
            return WindowNames;
        }
        
        public String[] GetWindowList(AutomationElement ParentWindowElement)
        {
            Int32 Cnt = 0;
            AutomationElementCollection aecWinAll = ParentWindowElement.FindAll(TreeScope.Children,
                new PropertyCondition(AutomationElement.IsContentElementProperty, true));
            String[] WindowNames = new String[aecWinAll.Count];
            foreach (AutomationElement aeWin in aecWinAll)
            {
                WindowNames[Cnt++] = aeWin.Current.Name.ToString();

            }
            return WindowNames;
        }
        
        public AutomationElement GetWindowByName(String WindowName, AutomationElement ParentWindowElement)
        {

            WindowName = WindowName.ToUpper().Trim();

            AutomationElementCollection aecWinAll = ParentWindowElement.FindAll(TreeScope.Children,
                new PropertyCondition(AutomationElement.IsContentElementProperty, true));
            foreach (AutomationElement aeWin in aecWinAll)
            {
                if (aeWin.Current.Name.ToString().ToUpper().IndexOf(WindowName) >= 0)
                {
                    return aeWin;
                }
            }

            return null;

        }
        
        public AutomationElement GetWindowByName(String WindowName)
        {

            WindowName = WindowName.ToUpper().Trim();

            AutomationElementCollection aecWinAll = Desktop.FindAll(TreeScope.Children,
                new PropertyCondition(AutomationElement.IsContentElementProperty, true));
            foreach (AutomationElement aeWin in aecWinAll)
            {
                if (aeWin.Current.Name.ToString().ToUpper().IndexOf(WindowName) >= 0)
                {
                    return aeWin;
                }
            }

            return null;

        }
        
        public AutomationElement GetMenuBar(AutomationElement ApplicationWindowElement)
        {

            try
            {
                AutomationElementCollection aecElementAll = ApplicationWindowElement.FindAll(TreeScope.Children,
                    new PropertyCondition(AutomationElement.IsEnabledProperty, true));
                foreach (AutomationElement aeElement in aecElementAll)
                {
                    if (aeElement.Current.ControlType == ControlType.MenuBar)
                    {
                        return aeElement;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }

        }
         
        public AutomationElement[] GetAllMenus(AutomationElement MenuBarElement)
        {
            if (MenuBarElement.Current.ControlType != ControlType.MenuBar)
            {
                return null;
            }

            Int32 Cnt = 0;
            AutomationElementCollection aecElementAll = MenuBarElement.FindAll(TreeScope.Children, 
                new PropertyCondition(AutomationElement.IsEnabledProperty, true));
            AutomationElement[] Elements = new AutomationElement[aecElementAll.Count];
            foreach (AutomationElement aeElement in aecElementAll)
            {
                Elements[Cnt++] = aeElement;
            }

            return Elements;
        }
         
        public AutomationElement GetMenuByName(String MenuName, AutomationElement MenuBarElement)
        {
            if (MenuBarElement.Current.ControlType != ControlType.MenuBar)
            {
                return null;
            }
            MenuName = MenuName.ToUpper();
            AutomationElementCollection aecElementAll = MenuBarElement.FindAll(TreeScope.Children, 
                new PropertyCondition(AutomationElement.IsEnabledProperty, true));
            foreach (AutomationElement aeElement in aecElementAll)
            {
                if (aeElement.Current.Name.ToString().ToUpper().IndexOf(MenuName) >= 0)
                {
                    return aeElement;
                }
            }

            return null;
        }
         
        public AutomationElement[] GetAllElement(AutomationElement ParentElement)
        {
            Int32 Cnt = 0;
            AutomationElementCollection aecElementAll = ParentElement.FindAll(TreeScope.Children, 
                new PropertyCondition(AutomationElement.IsEnabledProperty, true));
            AutomationElement[] Elements = new AutomationElement[aecElementAll.Count];
            foreach (AutomationElement aeElement in aecElementAll)
            {
                Elements[Cnt++] = aeElement;
            }

            return Elements;

        }
        
        public AutomationElement GetElementByName(String ElementName, AutomationElement ParentElement)
        {
            ElementName = ElementName.ToUpper();
            AutomationElementCollection aecElementAll = ParentElement.FindAll(TreeScope.Children, 
                new PropertyCondition(AutomationElement.IsEnabledProperty, true));
            foreach (AutomationElement aeElement in aecElementAll)
            {
                if (aeElement.Current.Name.ToString().ToUpper().IndexOf(ElementName) >= 0)
                {
                    return aeElement;
                }
            }

            return null;

        }
        
        public AutomationElement GetElementByID(String ElementID, AutomationElement ParentElement)
        {
            AutomationElement aeElement = ParentElement.FindFirst(TreeScope.Children,
                new PropertyCondition(AutomationElement.AutomationIdProperty, ElementID));
            return aeElement;
        }
        
        public AutomationElement GetSubMenuByName(String SubMenuName, AutomationElement MainMenuElement)
        {
            if (MainMenuElement.Current.ControlType != ControlType.MenuItem)
            {
                return null;
            }
            SubMenuName = SubMenuName.ToUpper();
            try
            {
                AutomationElement aeElement = MainMenuElement;
                ExpandCollapsePattern ee = (ExpandCollapsePattern)aeElement.GetCurrentPattern(ExpandCollapsePattern.Pattern);
                ee.Expand();

                TreeWalker X = TreeWalker.RawViewWalker;
                aeElement = X.GetFirstChild(aeElement);
                aeElement = X.GetFirstChild(aeElement);
                while (aeElement != null)
                {
                    try
                    {
                        if (aeElement.Current.Name.ToString().ToUpper().IndexOf(SubMenuName) >= 0)
                        {
                            return aeElement;
                        }
                        aeElement = X.GetNextSibling(aeElement);
                    }
                    catch
                    {
                        break;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }

        }
        
        public AutomationElement[] GetAllSubMenus(AutomationElement MenuElement)
        {
            AutomationElement[] aeAll = null;
            Int32 Cnt = 0;
            AutomationElement aeX = MenuElement;
            ExpandCollapsePattern ee = (ExpandCollapsePattern)MenuElement.GetCurrentPattern(ExpandCollapsePattern.Pattern);
            ee.Expand();
            {
                TreeWalker X = TreeWalker.RawViewWalker;
                aeX = X.GetFirstChild(MenuElement);
                aeX = X.GetFirstChild(aeX);
                while (aeX != null)
                {
                    try
                    { 
                        Cnt++;
                        aeX = X.GetNextSibling(aeX);
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            aeAll = new AutomationElement[Cnt + 1];
            Cnt = 0;
            {
                TreeWalker X = TreeWalker.RawViewWalker;
                aeX = X.GetFirstChild(MenuElement);
                aeX = X.GetFirstChild(aeX);
                while (aeX != null)
                {
                    try
                    { 
                        aeAll[Cnt++] = aeX;
                        aeX = X.GetNextSibling(aeX);
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            return aeAll;

        }
        
        public Boolean ClickElement(AutomationElement Element)
        {
            try
            {
                InvokePattern ipElement = (InvokePattern)Element.GetCurrentPattern(InvokePattern.Pattern);
                ipElement.Invoke();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public AutomationElement RefindMainApplication(AutomationElement ApplicationElement)
        {
            return GetWindowByName(ApplicationElement.Current.Name);
        }
        
        public Boolean FocusWindow(AutomationElement WindowElement)
        {
            if (WindowElement.Current.ControlType != ControlType.Window)
            {
                return false;
            }
            try
            {
                WindowPattern wpWin = (WindowPattern)WindowElement.GetCurrentPattern(WindowPattern.Pattern);
                wpWin.SetWindowVisualState(WindowVisualState.Maximized);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public String GetValue(AutomationElement Element)
        {
            try
            {
                ValuePattern vpElement = (ValuePattern)Element.GetCurrentPattern(ValuePattern.Pattern);
                return vpElement.Current.Value.ToString();
            }
            catch
            {
                return null;
            }
        }
        
        public Boolean SetValue(String Value, AutomationElement Element)
        {
            try
            {
                ValuePattern vpElement = (ValuePattern)Element.GetCurrentPattern(ValuePattern.Pattern);
                vpElement.SetValue(Value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public ControlType GetControlType(AutomationElement Element)
        {
            try
            {
                return Element.Current.ControlType;
            }
            catch
            {
                return null;
            }
        }
        
        public Boolean SelectValueInComboBox(String Value, AutomationElement ComboBoxElement)
        {
            if (GetControlType(ComboBoxElement) != ControlType.ComboBox && GetControlType(ComboBoxElement) != ControlType.List)
            {
                return false;
            }

            try
            {
                AutomationElement [] aeList = GetAllElement(ComboBoxElement);
                AutomationElement aeL = null;
                for (Int32 I = 0; I < aeList.Length; I++)
                {
                    if (GetControlType(aeList[I]) == ControlType.List)
                    {
                        aeL = aeList[I];
                        break;
                    }
                }

                if (aeL != null)
                {
                    AutomationElement aeListItem = GetElementByName(Value, aeL);
                    SelectionItemPattern sipListItem = (SelectionItemPattern)aeListItem.GetCurrentPattern(SelectionItemPattern.Pattern);
                    sipListItem.Select();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public Boolean SelectValueInListBox(String Value, AutomationElement ListBoxElement)
        {
            if (GetControlType(ListBoxElement) != ControlType.List && GetControlType(ListBoxElement) != ControlType.List)
            {
                return false;
            }

            try
            {
                AutomationElement aeListItem = GetElementByName(Value, ListBoxElement);
                SelectionItemPattern sipListItem = (SelectionItemPattern)aeListItem.GetCurrentPattern(SelectionItemPattern.Pattern);
                sipListItem.Select();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public String[] GetAllElementDetails(AutomationElement Element)
        {
            try
            {
                AutomationElementCollection aecWinAll = Element.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.IsEnabledProperty, true));
                String[] ElementNames = new String[aecWinAll.Count];
                Int32 Cnt = 0;
                foreach (AutomationElement aeWin in aecWinAll)
                {
                    ElementNames[Cnt++] = "Automation ID : " + aeWin.Current.AutomationId.ToString() + ", Control Type : " + aeWin.Current.ControlType.ProgrammaticName.ToString() + ", Name : " + aeWin.Current.Name;
                }
                return ElementNames;
            }
            catch
            {
                return null;
            }
        }
        
        public AutomationElement[] GetElementsByControlType(TypeOfControl CtrlType, AutomationElement ParentElement)
        {
            Int32 Cnt = 0;
            ControlType MyCtrlType = GetControlType(CtrlType);
            AutomationElement[] aeTemp = null;
            AutomationElement[] aecAll = GetAllElement(ParentElement);
            foreach (AutomationElement aeElement in aecAll)
            {
                if (aeElement.Current.ControlType == MyCtrlType)
                {

                    Cnt++;
                    
                }

            }

            aeTemp = new AutomationElement[Cnt];
            Cnt = 0;
            foreach (AutomationElement aeElement in aecAll)
            {
                if (aeElement.Current.ControlType == MyCtrlType)
                {
                    
                    aeTemp[Cnt] = aeElement;
                    Cnt++;

                }

            }
            
            return aeTemp;

        }

        public AutomationElement GetElementByContrlTypeAndName(TypeOfControl CtrlType, string name, AutomationElement ParentElement)
        {
            AutomationElement[] aeTemp = null;

            aeTemp = GetElementsByControlType(CtrlType, ParentElement);

            foreach (AutomationElement ae in aeTemp)
            {
                if (ae.Current.Name == name)
                {
                    return ae;
                }
            }
            return null;
        }
        
        public AutomationElement GetHeaderFromDataGrid(AutomationElement DataGrid)
        {
            if (DataGrid.Current.ControlType == ControlType.DataGrid)
            {
                AutomationElement[] aeHeader = GetElementsByControlType(TypeOfControl.Header, DataGrid);
                if (aeHeader.Length > 0)
                {
                    return aeHeader[0];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        
        public AutomationElement[] GetGridLinesFromDataGrid(AutomationElement DataGrid)
        {
            if (DataGrid.Current.ControlType == ControlType.DataGrid)
            {
                return GetElementsByControlType(TypeOfControl.GridLine, DataGrid);
                
            }
            else
            {
                return null;
            }
        }
        
        public AutomationElement[] GetColumnsFromGridLine(AutomationElement GridLine)
        {
            if (GridLine.Current.ControlType == ControlType.DataItem)
            {
                return GetElementsByControlType(TypeOfControl.TextBox, GridLine);

            }
            else
            {
                return null;
            }
        }
        
        public String GetName(AutomationElement Element)
        {
            try
            {
                return Element.Current.Name.ToString().Trim();
            }
            catch
            {
                return null;
            }
        }
        
        public String[] GetColumnValuesFromGridLine(AutomationElement GridLine)
        {
            if (GridLine.Current.ControlType == ControlType.DataItem)
            {
                AutomationElement [] aeTemp = GetElementsByControlType(TypeOfControl.TextBox, GridLine);
                String[] Names = new String[aeTemp.Length];
                Int32 Cnt = 0;
                
                foreach (AutomationElement aeT in aeTemp)
                {
                    Names[Cnt++] = GetName(aeT);
                }

                return Names;
            }
            else
            {
                return null;
            }
        }
        
        public Boolean SelectAllDataGridRow(AutomationElement DataGrid)
        {
            try
            {
                AutomationElement[] aeRows = GetElementsByControlType(TypeOfControl.GridLine, DataGrid);
                if (aeRows.Length > 0)
                {
                    SelectDataGridRow(aeRows[0]);
                    foreach (AutomationElement aeRow in aeRows)
                    {
                        SelectDataGridRow(aeRow, true);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public Boolean SelectDataGridRow(AutomationElement DataGridRow)
        {
            try
            {
                SelectionItemPattern spOne = (SelectionItemPattern)DataGridRow.GetCurrentPattern(SelectionItemPattern.Pattern);
                spOne.Select();
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public Boolean DisselectDataGridRow(AutomationElement DataGridRow)
        {
            try
            {
                SelectionItemPattern spOne = (SelectionItemPattern)DataGridRow.GetCurrentPattern(SelectionItemPattern.Pattern);
                spOne.RemoveFromSelection();
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public Boolean DisselectAllDataGridRow(AutomationElement DataGrid)
        {
            try
            {
                AutomationElement[] aeRows = GetElementsByControlType(TypeOfControl.GridLine, DataGrid);
                if (aeRows.Length > 0)
                {
                    
                    foreach (AutomationElement aeRow in aeRows)
                    {
                        DisselectDataGridRow(aeRow);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public Boolean SelectDataGridRow(AutomationElement DataGridRow, Boolean AddToSelection)
        {
            try
            {
                SelectionItemPattern spOne = (SelectionItemPattern)DataGridRow.GetCurrentPattern(SelectionItemPattern.Pattern);
                if (AddToSelection)
                {
                    spOne.AddToSelection();
                }
                else
                {
                    spOne.Select();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public String GetDocumentText(AutomationElement DocumentElement, Int32 TextMaxLength)
        {
            try
            {
                TextPattern tpText = (TextPattern)DocumentElement.GetCurrentPattern(TextPattern.Pattern);
                return (tpText.DocumentRange.GetText(TextMaxLength));

            }
            catch
            {
                return null;
            }
        }
        
        public ControlType GetControlType(TypeOfControl controlType)
        { 
            switch (controlType)
            {
                case TypeOfControl.TextBox:
                    {
                        return ControlType.Edit;
                    }
                case TypeOfControl.ComboBox:
                    {
                        return ControlType.ComboBox;
                    }
                case TypeOfControl.Grid:
                    {
                        return ControlType.DataGrid;
                    }
                case TypeOfControl.GridLine:
                    {
                        return ControlType.DataItem;
                    }
                case TypeOfControl.Window:
                    {
                        return ControlType.Window;
                    }
                case TypeOfControl.MenuBar:
                    {
                        return ControlType.MenuBar;
                    }
                case TypeOfControl.Menu:
                    {
                        return ControlType.Menu;
                    }
                case TypeOfControl.SubMenu:
                    {
                        return ControlType.MenuItem;
                    }
                case TypeOfControl.Panel:
                    {
                        return ControlType.Pane;
                    }
                case TypeOfControl.CheckBox:
                    {
                        return ControlType.CheckBox;
                    }
                case TypeOfControl.Header:
                    {
                        return ControlType.Header;
                    }
                case TypeOfControl.Text:
                    {
                        return ControlType.Text;
                    }
                case TypeOfControl.Document:
                    {
                        return ControlType.Document;
                    }
                case TypeOfControl.ToolBar:
                    {
                        return ControlType.ToolBar;
                    }
                case TypeOfControl.Button:
                    {
                        return ControlType.Button;
                    }
                case TypeOfControl.Custom:
                   {
                            return ControlType.Custom;
                   }
                case TypeOfControl.HeaderItem:
                   {
                        return ControlType.HeaderItem;
                   }
                case TypeOfControl.Tree:
                   {
                       return ControlType.Tree;
                   }
                case TypeOfControl.TreeItem:
                   {
                       return ControlType.TreeItem;
                   }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
