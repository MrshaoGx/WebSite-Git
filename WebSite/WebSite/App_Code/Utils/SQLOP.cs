﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebSite.App_Code.Obj.CampusTalk;

namespace WebSite.App_Code.Utils
{
    public class SQLOP
    {
        static SQLOP mSqlop = null;
        public static SQLOP getInstance()
        {
            if (mSqlop == null)
                mSqlop = new SQLOP();
            return mSqlop;
        }
        /// <summary>
        /// 检查邮件是否已存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckEmailValidate(string email)
        {
            string sql = "select * from " + GlobalVar.User.TABLE_USER + " where " + GlobalVar.User.EMAIL + "='" + email + "'";
            DataTable dt_email = ctSqlHelper.getInstance().Query(sql);
            if (dt_email.Rows.Count <= 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int AddUser(CTPerson user)
        {
            string sql = "insert into " + GlobalVar.User.TABLE_USER + " (" + GlobalVar.User.UID + "," + GlobalVar.User.PASSWORD + "," + GlobalVar.User.SEX + "," + GlobalVar.User.EMAIL + "," + GlobalVar.User.SCHOOLCODE + ") values('" + user.Uid + "','" + user.Password + "','" + user.Sex + "','" + user.Email + "','" + user.School.SCode + "')";
            try
            {
                return ctSqlHelper.getInstance().executeSql(sql);
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            return 0;
        }

        public string getSchoolNameBySchoolCode(string scode)
        {
            string sql = "select " + GlobalVar.SchoolInfo.SCHOOLNAME + " from " + GlobalVar.SchoolInfo.TABLE_SCHOOLINFO + " where " + GlobalVar.SchoolInfo.SCHOOLCODE + "='" + scode + "'";
            DataTable dt_sname = ctSqlHelper.getInstance().Query(sql);
            if (dt_sname.Rows.Count > 0)
            {
                return dt_sname.Rows[0][0].ToString();
            }
            return "";
        }

        /// <summary>
        /// 获取关注用户列表
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ArrayList getFollowlist(string uid)
        {
            ArrayList list = new ArrayList();
            string sql = "select a.* from " + GlobalVar.User.TABLE_USER + " a left join " + GlobalVar.Follow.TABLE_FOLLOW + " b on a." + GlobalVar.User.UID + "=b." + GlobalVar.Follow.FOLLOW + " where b." + GlobalVar.Follow.UID + "='" + uid + "'";
            try
            {
                DataTable dt_users = ctSqlHelper.getInstance().Query(sql);
                if (dt_users.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_users.Rows.Count; i++)
                    {
                        CTPerson user = new CTPerson();
                        user.Uid = dt_users.Rows[i][GlobalVar.User.UID].ToString();
                        user.Sex = dt_users.Rows[i][GlobalVar.User.SEX].ToString();
                        user.Nickname = dt_users.Rows[i][GlobalVar.User.NICKNAME].ToString();
                        CTSchool tmp_school = new CTSchool();
                        tmp_school.SCode = dt_users.Rows[i][GlobalVar.User.UID].ToString();
                        tmp_school.SName = getSchoolNameBySchoolCode(tmp_school.SCode);
                        user.School = tmp_school;
                        user.Email = dt_users.Rows[i][GlobalVar.User.EMAIL].ToString();
                        user.Headpic = dt_users.Rows[i][GlobalVar.User.HEADPIC].ToString();
                        user.Userexplain = dt_users.Rows[i][GlobalVar.User.USEREXPLAIN].ToString();
                        user.State = dt_users.Rows[i][GlobalVar.User.STATE].ToString();
                        user.Age = dt_users.Rows[i][GlobalVar.User.AGE].ToString();
                        list.Add(user);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return list;
        }
        /// <summary>
        /// 查询单个用户信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public CTPerson getUserProfile(string uid)
        {
            CTPerson user = new CTPerson();
            try
            {
                string sql = "select * from " + GlobalVar.User.TABLE_USER + " where " + GlobalVar.User.UID + "='" + uid + "'";
                DataTable dt_users = ctSqlHelper.getInstance().Query(sql);
                if (dt_users.Rows.Count > 0)
                {
                    user.Uid = dt_users.Rows[0][GlobalVar.User.UID].ToString();
                    user.Sex = dt_users.Rows[0][GlobalVar.User.SEX].ToString();
                    user.Nickname = dt_users.Rows[0][GlobalVar.User.NICKNAME].ToString();
                    CTSchool tmp_school = new CTSchool();
                    tmp_school.SCode = dt_users.Rows[0][GlobalVar.User.UID].ToString();
                    tmp_school.SName = getSchoolNameBySchoolCode(tmp_school.SCode);
                    user.School = tmp_school;
                    user.Email = dt_users.Rows[0][GlobalVar.User.EMAIL].ToString();
                    user.Headpic = dt_users.Rows[0][GlobalVar.User.HEADPIC].ToString();
                    user.Userexplain = dt_users.Rows[0][GlobalVar.User.USEREXPLAIN].ToString();
                    user.State = dt_users.Rows[0][GlobalVar.User.STATE].ToString();
                    user.Age = dt_users.Rows[0][GlobalVar.User.AGE].ToString();
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return user;
        }
        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        public int cancelfollow(string mid, string tid)
        {

            string sql = "delete from  " + GlobalVar.Follow.TABLE_FOLLOW + " where " + GlobalVar.Follow.UID + "='" + mid + "' and " + GlobalVar.Follow.FOLLOW + "='" + tid + "'";
            try
            {
                return ctSqlHelper.getInstance().executeSql(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }
        /// <summary>
        /// 关注
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        public int followtid(string mid, string tid)
        {
            string sql = "insert into " + GlobalVar.Follow.TABLE_FOLLOW + " (" + GlobalVar.Follow.UID + "," + GlobalVar.Follow.FOLLOW + "," + GlobalVar.Follow.TIME + ") values('" + mid + "','" + tid + "','" + DateTime.Now.ToShortDateString() + "')";
            try
            {
                return ctSqlHelper.getInstance().executeSql(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }
        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int AddSign(string uid)
        {
            string sql = "insert into " + GlobalVar.Sign.TABLE_SIGN + " (" + GlobalVar.Sign.UID + "," + GlobalVar.Sign.TIME + ") values('" + uid + "','" + DateTime.Now.Day + "')";
            try
            {
                return ctSqlHelper.getInstance().executeSql(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }
        /// <summary>
        /// 更新用个人资料
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int updateProfile(CTPerson user)
        {
            if (user==null) {
                return 0;
            }
            string sql = "update " + GlobalVar.User.TABLE_USER + " set " + GlobalVar.User.AGE + "='" + user.Age + "',"+ 
                                                                            GlobalVar.User.SEX + "='" + user.Sex + "',"+
                                                                                GlobalVar.User.SCHOOLCODE + "='" + user.School.SCode + "',"+ 
                                                                                GlobalVar.User.NICKNAME + "='" + user.Nickname + "',"+ 
                                                                                GlobalVar.User.HEADPIC + "='" + user.Headpic + "',"+ 
                                                                                GlobalVar.User.USEREXPLAIN + "='" + user.Userexplain + "' where" + 
                                                                                GlobalVar.User.UID+"='"+user.Uid+"'";
            try
            {
                return ctSqlHelper.getInstance().executeSql(sql);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            } 
            return 0;

        }
        /// <summary>
        /// 
        /// 上传并更新学生卡信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int updateStucard(string path,string uid) {
            string sql = "update "+GlobalVar.User.TABLE_USER+" set "+GlobalVar.User.STUCARD+"='"+path+"' where "+GlobalVar.User.UID+"='"+uid+"'";
            try
            {
                return ctSqlHelper.getInstance().executeSql(sql);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return 0;
        }

    }
}