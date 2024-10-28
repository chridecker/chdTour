﻿using Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chdTour.App.Platforms.Android
{
    public class PermissionValidator : Permissions.BasePlatformPermission
    {
        public override (string androidPermission, bool isRuntime)[] RequiredPermissions
        {
            get
            {
                var result = new List<(string androidPermission, bool isRuntime)>();
                if (OperatingSystem.IsAndroidVersionAtLeast(33))
                {
                    result.Add((Manifest.Permission.ReadExternalStorage, true));
                    result.Add((Manifest.Permission.WriteExternalStorage, true));
                    result.Add((Manifest.Permission.MediaContentControl, true));
                    result.Add((Manifest.Permission.PostNotifications, true));
                }
                return result.ToArray();
            }
        }
    }
}
