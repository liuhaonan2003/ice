// **********************************************************************
//
// Copyright (c) 2003 - 2004
// ZeroC, Inc.
// North Palm Beach, FL, USA
//
// All Rights Reserved.
//
// This copy of Ice is licensed to you under the terms described in the
// ICE_LICENSE file included in this distribution.
//
// **********************************************************************


public class Client
{
    public static void Main(string[] args)
    {
        NestedClient app = new NestedClient();
        int status = app.main(args, "config");
        System.Environment.Exit(status);
    }
}
