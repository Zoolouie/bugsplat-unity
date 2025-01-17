﻿using System;
using UnityEngine;

namespace BugSplatUnity.Runtime.Util
{
    public static class ShouldPostExceptionImpl
    {
        private static DateTime lastPost;

        public static bool DefaultShouldPostExceptionImpl(Exception ex = null)
        {
            if (lastPost + TimeSpan.FromSeconds(10) > DateTime.Now)
            {
                Debug.Log("BugSplat info: Report rate-limiting triggered, skipping report...");
                return false;
            }

            lastPost = DateTime.Now;
            return true;
        }
    }
}
