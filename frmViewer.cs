using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImgViewer
{
    public partial class frmViewer : Form
    {
        // 전역변수 선언
        int m_nCnt;
        int m_nSelLabel;

        public frmViewer()
        {
            InitializeComponent();
        }


        // 드라이브 목록 얻어서 콤보박스에 입력
        // 반환값 : 드라이브 갯수
        private int SetDrive()
        {
            String[] strDrives;
            String strTmp;

            // Up 항목을 추가한다. 
            lstDir.Items.Add("..", 0);

            // 드라이브 목록을 얻는다.
            strDrives = System.IO.Directory.GetLogicalDrives();

            // 얻어진 드라이브 목록을 리스트뷰에 입력한다.
            foreach ( string str in strDrives)
            {
                strTmp = str.Remove(2, 1);
                lstDir.Items.Add(strTmp, 1);
            }

            return lstDir.Items.Count;
        }


        // 하위 폴더를 화면에 출력한다.
        // 반환값 : 성공, 실패
        private bool SetFolder(String strParentPath)
        {
            System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(strParentPath);

            // 리스트뷰의 아이템을 모두 삭제한 후
            lstDir.Items.Clear();

            // 드라이브의 정보를 보여준다.
            m_nCnt = SetDrive();
            lblPath.Text = "";

            try
            {
                foreach (System.IO.DirectoryInfo dirInfoCurDir in dirInfo.GetDirectories("*"))
                {
                    lstDir.Items.Add(dirInfoCurDir.Name.ToString(), 2);
                }
                lblPath.Text = strParentPath.Remove(strParentPath.Length - 1, 1);
            }
            catch
            {
                MessageBox.Show("접근에 실패했습니다.");
                return false;
            }

            return true;

        }
    }
}
