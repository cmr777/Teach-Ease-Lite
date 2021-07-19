using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

public static class ProjectEnum
{
    public enum RoleType
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Instructor")]
        Instructor = 2,
        [Description("Viewer")]
        Viewer = 3
    }
}