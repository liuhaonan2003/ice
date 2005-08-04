// **********************************************************************
//
// Copyright (c) 2003-2005 ZeroC, Inc. All rights reserved.
//
// This copy of Ice-E is licensed to you under the terms described in the
// ICEE_LICENSE file included in this distribution.
//
// **********************************************************************

#ifndef HELLO_I_H
#define HELLO_I_H

#include <Hello.h>

class HelloI : public ::Demo::Hello
{
public:

    virtual void sayHello(const Ice::Current&) const;
    virtual void shutdown(const Ice::Current&);
};

#endif
