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


using System;
using System.Diagnostics;
using System.Threading;

public class AllTests
{
    private static void test(bool b)
    {
        if(!b)
        {
            throw new System.Exception();
        }
    }
    
    private class Callback
    {
        internal Callback(object obj)
        {
            _called = false;
	    _obj = obj;
        }
        
        public virtual bool check()
        {
            lock(this)
            {
                while(!_called)
                {
                    try
                    {
                        Monitor.Wait(_obj, TimeSpan.FromMilliseconds(5000));
                    }
                    catch(ThreadInterruptedException)
                    {
                        continue;
                    }
                    
                    if(!_called)
                    {
                        return false; // Must be timeout.
                    }
                }
                
                _called = false;
                return true;
            }
        }
        
        public virtual void called()
        {
            lock(this)
            {
                Debug.Assert(!_called);
                _called = true;
                Monitor.Pulse(_obj);
            }
        }
        
        private bool _called;
	private object _obj;
    }
    
    /*
    private class AMI_Test_SBaseAsObjectI:AMI_Test_SBaseAsObject
    {
        public AMI_Test_SBaseAsObjectI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(Ice.Object o)
        {
            AllTests.test(o != null);
            AllTests.test(o.ice_id(null).Equals("::SBase"));
            SBase sb = (SBase) o;
            AllTests.test(sb != null);
            AllTests.test(sb.sb.Equals("SBase.sb"));
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_SBaseAsSBaseI:AMI_Test_SBaseAsSBase
    {
        public AMI_Test_SBaseAsSBaseI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(SBase sb)
        {
            AllTests.test(sb.sb.Equals("SBase.sb"));
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_SBSKnownDerivedAsSBaseI:AMI_Test_SBSKnownDerivedAsSBase
    {
        public AMI_Test_SBSKnownDerivedAsSBaseI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(SBase sb)
        {
            AllTests.test(sb.sb.Equals("SBSKnownDerived.sb"));
            SBSKnownDerived sbskd = (SBSKnownDerived) sb;
            AllTests.test(sbskd != null);
            AllTests.test(sbskd.sbskd.Equals("SBSKnownDerived.sbskd"));
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_SBSKnownDerivedAsSBSKnownDerivedI:AMI_Test_SBSKnownDerivedAsSBSKnownDerived
    {
        public AMI_Test_SBSKnownDerivedAsSBSKnownDerivedI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(SBSKnownDerived sbskd)
        {
            AllTests.test(sbskd.sbskd.Equals("SBSKnownDerived.sbskd"));
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_SBSUnknownDerivedAsSBaseI:AMI_Test_SBSUnknownDerivedAsSBase
    {
        public AMI_Test_SBSUnknownDerivedAsSBaseI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(SBase sb)
        {
            AllTests.test(sb.sb.Equals("SBSUnknownDerived.sb"));
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_SUnknownAsObjectI:AMI_Test_SUnknownAsObject
    {
        public AMI_Test_SUnknownAsObjectI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(Ice.Object o)
        {
            AllTests.test(o.ice_id(null).Equals("::Ice::Object"));
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_oneElementCycleI:AMI_Test_oneElementCycle
    {
        public AMI_Test_oneElementCycleI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(B b)
        {
            AllTests.test(b != null);
            AllTests.test(b.ice_id(null).Equals("::B"));
            AllTests.test(b.sb.Equals("B1.sb"));
            AllTests.test(b.pb == b);
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_twoElementCycleI:AMI_Test_twoElementCycle
    {
        public AMI_Test_twoElementCycleI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(B b1)
        {
            AllTests.test(b1 != null);
            AllTests.test(b1.ice_id(null).Equals("::B"));
            AllTests.test(b1.sb.Equals("B1.sb"));
            
            B b2 = b1.pb;
            AllTests.test(b2 != null);
            AllTests.test(b2.ice_id(null).Equals("::B"));
            AllTests.test(b2.sb.Equals("B2.sb"));
            AllTests.test(b2.pb == b1);
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_D1AsBI:AMI_Test_D1AsB
    {
        public AMI_Test_D1AsBI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(B b1)
        {
            AllTests.test(b1 != null);
            AllTests.test(b1.ice_id(null).Equals("::D1"));
            AllTests.test(b1.sb.Equals("D1.sb"));
            AllTests.test(b1.pb != null);
            AllTests.test(b1.pb != b1);
            D1 d1 = (D1) b1;
            AllTests.test(d1 != null);
            AllTests.test(d1.sd1.Equals("D1.sd1"));
            AllTests.test(d1.pd1 != null);
            AllTests.test(d1.pd1 != b1);
            AllTests.test(b1.pb == d1.pd1);
            
            B b2 = b1.pb;
            AllTests.test(b2 != null);
            AllTests.test(b2.pb == b1);
            AllTests.test(b2.sb.Equals("D2.sb"));
            AllTests.test(b2.ice_id(null).Equals("::B"));
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_D1AsD1I:AMI_Test_D1AsD1
    {
        public AMI_Test_D1AsD1I()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(D1 d1)
        {
            AllTests.test(d1 != null);
            AllTests.test(d1.ice_id(null).Equals("::D1"));
            AllTests.test(d1.sb.Equals("D1.sb"));
            AllTests.test(d1.pb != null);
            AllTests.test(d1.pb != d1);
            
            B b2 = d1.pb;
            AllTests.test(b2 != null);
            AllTests.test(b2.ice_id(null).Equals("::B"));
            AllTests.test(b2.sb.Equals("D2.sb"));
            AllTests.test(b2.pb == d1);
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_D2AsBI:AMI_Test_D2AsB
    {
        public AMI_Test_D2AsBI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(B b2)
        {
            AllTests.test(b2 != null);
            AllTests.test(b2.ice_id(null).Equals("::B"));
            AllTests.test(b2.sb.Equals("D2.sb"));
            AllTests.test(b2.pb != null);
            AllTests.test(b2.pb != b2);
            
            B b1 = b2.pb;
            AllTests.test(b1 != null);
            AllTests.test(b1.ice_id(null).Equals("::D1"));
            AllTests.test(b1.sb.Equals("D1.sb"));
            AllTests.test(b1.pb == b2);
            D1 d1 = (D1) b1;
            AllTests.test(d1 != null);
            AllTests.test(d1.sd1.Equals("D1.sd1"));
            AllTests.test(d1.pd1 == b2);
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_paramTest1I:AMI_Test_paramTest1
    {
        public AMI_Test_paramTest1I()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(B b1, B b2)
        {
            AllTests.test(b1 != null);
            AllTests.test(b1.ice_id(null).Equals("::D1"));
            AllTests.test(b1.sb.Equals("D1.sb"));
            AllTests.test(b1.pb == b2);
            D1 d1 = (D1) b1;
            AllTests.test(d1 != null);
            AllTests.test(d1.sd1.Equals("D1.sd1"));
            AllTests.test(d1.pd1 == b2);
            
            AllTests.test(b2 != null);
            AllTests.test(b2.ice_id(null).Equals("::B")); // No factory, must be sliced
            AllTests.test(b2.sb.Equals("D2.sb"));
            AllTests.test(b2.pb == b1);
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_paramTest2I:AMI_Test_paramTest2
    {
        public AMI_Test_paramTest2I()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(B b2, B b1)
        {
            AllTests.test(b1 != null);
            AllTests.test(b1.ice_id(null).Equals("::D1"));
            AllTests.test(b1.sb.Equals("D1.sb"));
            AllTests.test(b1.pb == b2);
            D1 d1 = (D1) b1;
            AllTests.test(d1 != null);
            AllTests.test(d1.sd1.Equals("D1.sd1"));
            AllTests.test(d1.pd1 == b2);
            
            AllTests.test(b2 != null);
            AllTests.test(b2.ice_id(null).Equals("::B")); // No factory, must be sliced
            AllTests.test(b2.sb.Equals("D2.sb"));
            AllTests.test(b2.pb == b1);
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_returnTest1I:AMI_Test_returnTest1
    {
        public AMI_Test_returnTest1I()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(B r, B p1, B p2)
        {
            AllTests.test(r == p1);
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_returnTest2I:AMI_Test_returnTest2
    {
        public AMI_Test_returnTest2I()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(B r, B p1, B p2)
        {
            AllTests.test(r == p1);
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_returnTest3I:AMI_Test_returnTest3
    {
        public AMI_Test_returnTest3I()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(B b)
        {
            r = b;
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
        
        public B r;
    }
    
    private class AMI_Test_paramTest3I:AMI_Test_paramTest3
    {
        public AMI_Test_paramTest3I()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(B ret, B p1, B p2)
        {
            AllTests.test(p1 != null);
            AllTests.test(p1.sb.Equals("D2.sb (p1 1)"));
            AllTests.test(p1.pb == null);
            AllTests.test(p1.ice_id(null).Equals("::B"));
            
            AllTests.test(p2 != null);
            AllTests.test(p2.sb.Equals("D2.sb (p2 1)"));
            AllTests.test(p2.pb == null);
            AllTests.test(p2.ice_id(null).Equals("::B"));
            
            AllTests.test(ret != null);
            AllTests.test(ret.sb.Equals("D1.sb (p2 2)"));
            AllTests.test(ret.pb == null);
            AllTests.test(ret.ice_id(null).Equals("::D1"));
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
        
        public B r;
    }
    
    private class AMI_Test_paramTest4I:AMI_Test_paramTest4
    {
        public AMI_Test_paramTest4I()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(B ret, B b)
        {
            AllTests.test(b != null);
            AllTests.test(b.sb.Equals("D4.sb (1)"));
            AllTests.test(b.pb == null);
            AllTests.test(b.ice_id(null).Equals("::B"));
            
            AllTests.test(ret != null);
            AllTests.test(ret.sb.Equals("B.sb (2)"));
            AllTests.test(ret.pb == null);
            AllTests.test(ret.ice_id(null).Equals("::B"));
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
        
        public B r;
    }
    
    private class AMI_Test_sequenceTestI:AMI_Test_sequenceTest
    {
        public AMI_Test_sequenceTestI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(SS ss)
        {
            r = ss;
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
        
        public SS r;
    }
    
    private class AMI_Test_dictionaryTestI:AMI_Test_dictionaryTest
    {
        public AMI_Test_dictionaryTestI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        //UPGRADE_TODO: Interface java.util was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1095"'
        public virtual void  ice_response(java.util.Map r, java.util.Map bout)
        {
            this.r = r;
            this.bout = bout;
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
        
        //UPGRADE_TODO: Interface java.util was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1095"'
        public java.util.Map r;
        //UPGRADE_TODO: Interface java.util was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1095"'
        public java.util.Map bout;
    }
    
    private class AMI_Test_throwBaseAsBaseI:AMI_Test_throwBaseAsBase
    {
        public AMI_Test_throwBaseAsBaseI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response()
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            try
            {
                BaseException e = (BaseException) exc;
                AllTests.test(e.ice_name().Equals("BaseException"));
                AllTests.test(e.sbe.Equals("sbe"));
                AllTests.test(e.pb != null);
                AllTests.test(e.pb.sb.Equals("sb"));
                AllTests.test(e.pb.pb == e.pb);
            }
            catch(Exception e)
            {
                AllTests.test(false);
            }
            callback.called();
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_throwDerivedAsBaseI:AMI_Test_throwDerivedAsBase
    {
        public AMI_Test_throwDerivedAsBaseI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response()
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            try
            {
                DerivedException e = (DerivedException) exc;
                AllTests.test(e.ice_name().Equals("DerivedException"));
                AllTests.test(e.sbe.Equals("sbe"));
                AllTests.test(e.pb != null);
                AllTests.test(e.pb.sb.Equals("sb1"));
                AllTests.test(e.pb.pb == e.pb);
                AllTests.test(e.sde.Equals("sde1"));
                AllTests.test(e.pd1 != null);
                AllTests.test(e.pd1.sb.Equals("sb2"));
                AllTests.test(e.pd1.pb == e.pd1);
                AllTests.test(e.pd1.sd1.Equals("sd2"));
                AllTests.test(e.pd1.pd1 == e.pd1);
            }
            catch(Exception e)
            {
                AllTests.test(false);
            }
            callback.called();
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_throwDerivedAsDerivedI:AMI_Test_throwDerivedAsDerived
    {
        public AMI_Test_throwDerivedAsDerivedI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response()
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            try
            {
                DerivedException e = (DerivedException) exc;
                AllTests.test(e.ice_name().Equals("DerivedException"));
                AllTests.test(e.sbe.Equals("sbe"));
                AllTests.test(e.pb != null);
                AllTests.test(e.pb.sb.Equals("sb1"));
                AllTests.test(e.pb.pb == e.pb);
                AllTests.test(e.sde.Equals("sde1"));
                AllTests.test(e.pd1 != null);
                AllTests.test(e.pd1.sb.Equals("sb2"));
                AllTests.test(e.pd1.pb == e.pd1);
                AllTests.test(e.pd1.sd1.Equals("sd2"));
                AllTests.test(e.pd1.pd1 == e.pd1);
            }
            catch(Exception e)
            {
                AllTests.test(false);
            }
            callback.called();
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_throwUnknownDerivedAsBaseI:AMI_Test_throwUnknownDerivedAsBase
    {
        public AMI_Test_throwUnknownDerivedAsBaseI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response()
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            try
            {
                BaseException e = (BaseException) exc;
                AllTests.test(e.ice_name().Equals("BaseException"));
                AllTests.test(e.sbe.Equals("sbe"));
                AllTests.test(e.pb != null);
                AllTests.test(e.pb.sb.Equals("sb d2"));
                AllTests.test(e.pb.pb == e.pb);
            }
            catch(Exception e)
            {
                AllTests.test(false);
            }
            callback.called();
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    
    private class AMI_Test_useForwardI:AMI_Test_useForward
    {
        public AMI_Test_useForwardI()
        {
            InitBlock();
        }
        private void  InitBlock()
        {
            callback = new Callback();
        }
        public virtual void  ice_response(Forward f)
        {
            AllTests.test(f != null);
            callback.called();
        }
        
        public virtual void  ice_exception(Ice.LocalException exc)
        {
            AllTests.test(false);
        }
        
        public virtual void  ice_exception(Ice.UserException exc)
        {
            AllTests.test(false);
        }
        
        public virtual bool check()
        {
            return callback.check();
        }
        
        //UPGRADE_NOTE: The initialization of  'callback' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
        private Callback callback;
    }
    */
    
    public static TestPrx allTests(Ice.Communicator communicator, bool collocated)
    {
        Console.Out.Write("testing stringToProxy... ");
        Console.Out.Flush();
        string r = "Test:default -p 12345 -t 2000";
        Ice.ObjectPrx basePrx = communicator.stringToProxy(r);
        test(basePrx != null);
        Console.Out.WriteLine("ok");
        
        Console.Out.Write("testing checked cast... ");
        Console.Out.Flush();
        TestPrx testPrx = TestPrxHelper.checkedCast(basePrx);
        test(testPrx != null);
        test(testPrx.Equals(basePrx));
        Console.Out.WriteLine("ok");
        
        Console.Out.Write("base as Object... ");
        Console.Out.Flush();
        {
            Ice.Object o;
            SBase sb = null;
            try
            {
                o = testPrx.SBaseAsObject();
                test(o != null);
                test(o.ice_id(null).Equals("::SBase"));
                sb = (SBase) o;
            }
            catch(Exception)
            {
                test(false);
            }
            test(sb != null);
            test(sb.sb.Equals("SBase.sb"));
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("base as Object (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_SBaseAsObjectI cb = new AMI_Test_SBaseAsObjectI();
            testPrx.SBaseAsObject_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("base as base... ");
        Console.Out.Flush();
        {
            SBase sb;
            try
            {
                sb = testPrx.SBaseAsSBase();
                test(sb.sb.Equals("SBase.sb"));
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("base as base (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_SBaseAsSBaseI cb = new AMI_Test_SBaseAsSBaseI();
            testPrx.SBaseAsSBase_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("base with known derived as base... ");
        Console.Out.Flush();
        {
            SBase sb;
            SBSKnownDerived sbskd = null;
            try
            {
                sb = testPrx.SBSKnownDerivedAsSBase();
                test(sb.sb.Equals("SBSKnownDerived.sb"));
                sbskd = (SBSKnownDerived) sb;
            }
            catch(Exception)
            {
                test(false);
            }
            test(sbskd != null);
            test(sbskd.sbskd.Equals("SBSKnownDerived.sbskd"));
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("base with known derived as base (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_SBSKnownDerivedAsSBaseI cb = new AMI_Test_SBSKnownDerivedAsSBaseI();
            testPrx.SBSKnownDerivedAsSBase_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("base with known derived as known derived... ");
        Console.Out.Flush();
        {
            SBSKnownDerived sbskd;
            try
            {
                sbskd = testPrx.SBSKnownDerivedAsSBSKnownDerived();
                test(sbskd.sbskd.Equals("SBSKnownDerived.sbskd"));
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("base with known derived as known derived (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_SBSKnownDerivedAsSBSKnownDerivedI cb = new AMI_Test_SBSKnownDerivedAsSBSKnownDerivedI();
            testPrx.SBSKnownDerivedAsSBSKnownDerived_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("base with unknown derived as base... ");
        Console.Out.Flush();
        {
            SBase sb;
            try
            {
                sb = testPrx.SBSUnknownDerivedAsSBase();
                test(sb.sb.Equals("SBSUnknownDerived.sb"));
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("base with unknown derived as base (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_SBSUnknownDerivedAsSBaseI cb = new AMI_Test_SBSUnknownDerivedAsSBaseI();
            testPrx.SBSUnknownDerivedAsSBase_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("unknown with Object as Object... ");
        Console.Out.Flush();
        {
            Ice.Object o;
            try
            {
                o = testPrx.SUnknownAsObject();
                test(o.ice_id(null).Equals("::Ice::Object"));
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("unknown with Object as Object (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_SUnknownAsObjectI cb = new AMI_Test_SUnknownAsObjectI();
            testPrx.SUnknownAsObject_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("one-element cycle... ");
        Console.Out.Flush();
        {
            try
            {
                B b = testPrx.oneElementCycle();
                test(b != null);
                test(b.ice_id(null).Equals("::B"));
                test(b.sb.Equals("B1.sb"));
                test(b.pb == b);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("one-element cycle (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_oneElementCycleI cb = new AMI_Test_oneElementCycleI();
            testPrx.oneElementCycle_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("two-element cycle... ");
        Console.Out.Flush();
        {
            try
            {
                B b1 = testPrx.twoElementCycle();
                test(b1 != null);
                test(b1.ice_id(null).Equals("::B"));
                test(b1.sb.Equals("B1.sb"));
                
                B b2 = b1.pb;
                test(b2 != null);
                test(b2.ice_id(null).Equals("::B"));
                test(b2.sb.Equals("B2.sb"));
                test(b2.pb == b1);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("two-element cycle (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_twoElementCycleI cb = new AMI_Test_twoElementCycleI();
            testPrx.twoElementCycle_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("known derived pointer slicing as base... ");
        Console.Out.Flush();
        {
            try
            {
                B b1;
                b1 = testPrx.D1AsB();
                test(b1 != null);
                test(b1.ice_id(null).Equals("::D1"));
                test(b1.sb.Equals("D1.sb"));
                test(b1.pb != null);
                test(b1.pb != b1);
                D1 d1 = (D1) b1;
                test(d1 != null);
                test(d1.sd1.Equals("D1.sd1"));
                test(d1.pd1 != null);
                test(d1.pd1 != b1);
                test(b1.pb == d1.pd1);
                
                B b2 = b1.pb;
                test(b2 != null);
                test(b2.pb == b1);
                test(b2.sb.Equals("D2.sb"));
                test(b2.ice_id(null).Equals("::B"));
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("known derived pointer slicing as base (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_D1AsBI cb = new AMI_Test_D1AsBI();
            testPrx.D1AsB_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("known derived pointer slicing as derived... ");
        Console.Out.Flush();
        {
            try
            {
                D1 d1;
                d1 = testPrx.D1AsD1();
                test(d1 != null);
                test(d1.ice_id(null).Equals("::D1"));
                test(d1.sb.Equals("D1.sb"));
                test(d1.pb != null);
                test(d1.pb != d1);
                
                B b2 = d1.pb;
                test(b2 != null);
                test(b2.ice_id(null).Equals("::B"));
                test(b2.sb.Equals("D2.sb"));
                test(b2.pb == d1);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("known derived pointer slicing as derived (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_D1AsD1I cb = new AMI_Test_D1AsD1I();
            testPrx.D1AsD1_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("unknown derived pointer slicing as base... ");
        Console.Out.Flush();
        {
            try
            {
                B b2;
                b2 = testPrx.D2AsB();
                test(b2 != null);
                test(b2.ice_id(null).Equals("::B"));
                test(b2.sb.Equals("D2.sb"));
                test(b2.pb != null);
                test(b2.pb != b2);
                
                B b1 = b2.pb;
                test(b1 != null);
                test(b1.ice_id(null).Equals("::D1"));
                test(b1.sb.Equals("D1.sb"));
                test(b1.pb == b2);
                D1 d1 = (D1) b1;
                test(d1 != null);
                test(d1.sd1.Equals("D1.sd1"));
                test(d1.pd1 == b2);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("unknown derived pointer slicing as base (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_D2AsBI cb = new AMI_Test_D2AsBI();
            testPrx.D2AsB_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("param ptr slicing with known first... ");
        Console.Out.Flush();
        {
            try
            {
                B b1;
                B b2;
                testPrx.paramTest1(out b1, out b2);
                
                test(b1 != null);
                test(b1.ice_id(null).Equals("::D1"));
                test(b1.sb.Equals("D1.sb"));
                test(b1.pb == b2);
                D1 d1 = (D1) b1;
                test(d1 != null);
                test(d1.sd1.Equals("D1.sd1"));
                test(d1.pd1 == b2);
                
                test(b2 != null);
                test(b2.ice_id(null).Equals("::B")); // No factory, must be sliced
                test(b2.sb.Equals("D2.sb"));
                test(b2.pb == b1);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("param ptr slicing with known first (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_paramTest1I cb = new AMI_Test_paramTest1I();
            testPrx.paramTest1_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("param ptr slicing with unknown first... ");
        Console.Out.Flush();
        {
            try
            {
                B b2;
                B b1;
                testPrx.paramTest2(out b2, out b1);
                
                test(b1 != null);
                test(b1.ice_id(null).Equals("::D1"));
                test(b1.sb.Equals("D1.sb"));
                test(b1.pb == b2);
                D1 d1 = (D1) b1;
                test(d1 != null);
                test(d1.sd1.Equals("D1.sd1"));
                test(d1.pd1 == b2);
                
                test(b2 != null);
                test(b2.ice_id(null).Equals("::B")); // No factory, must be sliced
                test(b2.sb.Equals("D2.sb"));
                test(b2.pb == b1);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("param ptr slicing with unknown first (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_paramTest2I cb = new AMI_Test_paramTest2I();
            testPrx.paramTest2_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("return value identity with known first... ");
        Console.Out.Flush();
        {
            try
            {
                B p1;
                B p2;
                B ret = testPrx.returnTest1(out p1, out p2);
                test(ret == p1);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("return value identity with known first (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_returnTest1I cb = new AMI_Test_returnTest1I();
            testPrx.returnTest1_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("return value identity with unknown first... ");
        Console.Out.Flush();
        {
            try
            {
                B p1;
                B p2;
                B ret = testPrx.returnTest2(out p1, out p2);
                test(ret == p1);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("return value identity with unknown first (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_returnTest2I cb = new AMI_Test_returnTest2I();
            testPrx.returnTest2_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("return value identity for input params known first... ");
        Console.Out.Flush();
        {
            try
            {
                D1 d1 = new D1();
                d1.sb = "D1.sb";
                d1.sd1 = "D1.sd1";
                D3 d3 = new D3();
                d3.pb = d1;
                d3.sb = "D3.sb";
                d3.sd3 = "D3.sd3";
                d3.pd3 = d1;
                d1.pb = d3;
                d1.pd1 = d3;
                
                B b1 = testPrx.returnTest3(d1, d3);
                
                test(b1 != null);
                test(b1.sb.Equals("D1.sb"));
                test(b1.ice_id(null).Equals("::D1"));
                D1 p1 = (D1) b1;
                test(p1 != null);
                test(p1.sd1.Equals("D1.sd1"));
                test(p1.pd1 == b1.pb);
                
                B b2 = b1.pb;
                test(b2 != null);
                test(b2.sb.Equals("D3.sb"));
                test(b2.ice_id(null).Equals("::B")); // Sliced by server
                test(b2.pb == b1);
                bool gotException = false;
                try
                {
                    D3 p3 = (D3) b2;
                }
                catch(InvalidCastException)
                {
                    gotException = true;
                }
                test(gotException);
                
                test(b1 != d1);
                test(b1 != d3);
                test(b2 != d1);
                test(b2 != d3);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("return value identity for input params known first (AMI)... ");
        Console.Out.Flush();
        {
            D1 d1 = new D1();
            d1.sb = "D1.sb";
            d1.sd1 = "D1.sd1";
            D3 d3 = new D3();
            d3.pb = d1;
            d3.sb = "D3.sb";
            d3.sd3 = "D3.sd3";
            d3.pd3 = d1;
            d1.pb = d3;
            d1.pd1 = d3;
            
            AMI_Test_returnTest3I cb = new AMI_Test_returnTest3I();
            testPrx.returnTest3_async(cb, d1, d3);
            test(cb.check());
            B b1 = cb.r;
            
            test(b1 != null);
            test(b1.sb.Equals("D1.sb"));
            test(b1.ice_id(null).Equals("::D1"));
            D1 p1 = (D1) b1;
            test(p1 != null);
            test(p1.sd1.Equals("D1.sd1"));
            test(p1.pd1 == b1.pb);
            
            B b2 = b1.pb;
            test(b2 != null);
            test(b2.sb.Equals("D3.sb"));
            test(b2.ice_id(null).Equals("::B")); // Sliced by server
            test(b2.pb == b1);
            bool gotException = false;
            try
            {
                D3 p3 = (D3) b2;
            }
            catch(InvalidCastException)
            {
                gotException = true;
            }
            test(gotException);
            
            test(b1 != d1);
            test(b1 != d3);
            test(b2 != d1);
            test(b2 != d3);
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("return value identity for input params unknown first... ");
        Console.Out.Flush();
        {
            try
            {
                D1 d1 = new D1();
                d1.sb = "D1.sb";
                d1.sd1 = "D1.sd1";
                D3 d3 = new D3();
                d3.pb = d1;
                d3.sb = "D3.sb";
                d3.sd3 = "D3.sd3";
                d3.pd3 = d1;
                d1.pb = d3;
                d1.pd1 = d3;
                
                B b1 = testPrx.returnTest3(d3, d1);
                
                test(b1 != null);
                test(b1.sb.Equals("D3.sb"));
                test(b1.ice_id(null).Equals("::B")); // Sliced by server
                
                bool gotException = false;
                try
                {
                    D3 p1 = (D3) b1;
                }
                catch(InvalidCastException)
                {
                    gotException = true;
                }
                test(gotException);
                
                B b2 = b1.pb;
                test(b2 != null);
                test(b2.sb.Equals("D1.sb"));
                test(b2.ice_id(null).Equals("::D1"));
                test(b2.pb == b1);
                D1 p3 = (D1) b2;
                test(p3 != null);
                test(p3.sd1.Equals("D1.sd1"));
                test(p3.pd1 == b1);
                
                test(b1 != d1);
                test(b1 != d3);
                test(b2 != d1);
                test(b2 != d3);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("return value identity for input params unknown first (AMI)... ");
        Console.Out.Flush();
        {
            D1 d1 = new D1();
            d1.sb = "D1.sb";
            d1.sd1 = "D1.sd1";
            D3 d3 = new D3();
            d3.pb = d1;
            d3.sb = "D3.sb";
            d3.sd3 = "D3.sd3";
            d3.pd3 = d1;
            d1.pb = d3;
            d1.pd1 = d3;
            
            AMI_Test_returnTest3I cb = new AMI_Test_returnTest3I();
            testPrx.returnTest3_async(cb, d3, d1);
            test(cb.check());
            B b1 = cb.r;
            
            test(b1 != null);
            test(b1.sb.Equals("D3.sb"));
            test(b1.ice_id(null).Equals("::B")); // Sliced by server
            
            bool gotException = false;
            try
            {
                D3 p1 = (D3) b1;
            }
            catch(InvalidCastException)
            {
                gotException = true;
            }
            test(gotException);
            
            B b2 = b1.pb;
            test(b2 != null);
            test(b2.sb.Equals("D1.sb"));
            test(b2.ice_id(null).Equals("::D1"));
            test(b2.pb == b1);
            D1 p3 = (D1) b2;
            test(p3 != null);
            test(p3.sd1.Equals("D1.sd1"));
            test(p3.pd1 == b1);
            
            test(b1 != d1);
            test(b1 != d3);
            test(b2 != d1);
            test(b2 != d3);
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("remainder unmarshaling (3 instances)... ");
        Console.Out.Flush();
        {
            try
            {
                B p1;
                B p2;
                B ret = testPrx.paramTest3(out p1, out p2);
                
                test(p1 != null);
                test(p1.sb.Equals("D2.sb (p1 1)"));
                test(p1.pb == null);
                test(p1.ice_id(null).Equals("::B"));
                
                test(p2 != null);
                test(p2.sb.Equals("D2.sb (p2 1)"));
                test(p2.pb == null);
                test(p2.ice_id(null).Equals("::B"));
                
                test(ret != null);
                test(ret.sb.Equals("D1.sb (p2 2)"));
                test(ret.pb == null);
                test(ret.ice_id(null).Equals("::D1"));
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("remainder unmarshaling (3 instances) (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_paramTest3I cb = new AMI_Test_paramTest3I();
            testPrx.paramTest3_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("remainder unmarshaling (4 instances)... ");
        Console.Out.Flush();
        {
            try
            {
                B b;
                B ret = testPrx.paramTest4(out b);
                
                test(b != null);
                test(b.sb.Equals("D4.sb (1)"));
                test(b.pb == null);
                test(b.ice_id(null).Equals("::B"));
                
                test(ret != null);
                test(ret.sb.Equals("B.sb (2)"));
                test(ret.pb == null);
                test(ret.ice_id(null).Equals("::B"));
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("remainder unmarshaling (4 instances) (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_paramTest4I cb = new AMI_Test_paramTest4I();
            testPrx.paramTest4_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("param ptr slicing, instance marshaled in unknown derived as base... ");
        Console.Out.Flush();
        {
            try
            {
                B b1 = new B();
                b1.sb = "B.sb(1)";
                b1.pb = b1;
                
                D3 d3 = new D3();
                d3.sb = "D3.sb";
                d3.pb = d3;
                d3.sd3 = "D3.sd3";
                d3.pd3 = b1;
                
                B b2 = new B();
                b2.sb = "B.sb(2)";
                b2.pb = b1;
                
                B ret = testPrx.returnTest3(d3, b2);
                
                test(ret != null);
                test(ret.ice_id(null).Equals("::B"));
                test(ret.sb.Equals("D3.sb"));
                test(ret.pb == ret);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("param ptr slicing, instance marshaled in unknown derived as base (AMI)... ");
        Console.Out.Flush();
        {
            B b1 = new B();
            b1.sb = "B.sb(1)";
            b1.pb = b1;
            
            D3 d3 = new D3();
            d3.sb = "D3.sb";
            d3.pb = d3;
            d3.sd3 = "D3.sd3";
            d3.pd3 = b1;
            
            B b2 = new B();
            b2.sb = "B.sb(2)";
            b2.pb = b1;
            
            AMI_Test_returnTest3I cb = new AMI_Test_returnTest3I();
            testPrx.returnTest3_async(cb, d3, b2);
            test(cb.check());
            B r = cb.r;
            
            test(r != null);
            test(r.ice_id(null).Equals("::B"));
            test(r.sb.Equals("D3.sb"));
            test(r.pb == r);
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("param ptr slicing, instance marshaled in unknown derived as derived... ");
        Console.Out.Flush();
        {
            try
            {
                D1 d11 = new D1();
                d11.sb = "D1.sb(1)";
                d11.pb = d11;
                d11.sd1 = "D1.sd1(1)";
                
                D3 d3 = new D3();
                d3.sb = "D3.sb";
                d3.pb = d3;
                d3.sd3 = "D3.sd3";
                d3.pd3 = d11;
                
                D1 d12 = new D1();
                d12.sb = "D1.sb(2)";
                d12.pb = d12;
                d12.sd1 = "D1.sd1(2)";
                d12.pd1 = d11;
                
                B ret = testPrx.returnTest3(d3, d12);
                test(ret != null);
                test(ret.ice_id(null).Equals("::B"));
                test(ret.sb.Equals("D3.sb"));
                test(ret.pb == ret);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("param ptr slicing, instance marshaled in unknown derived as derived (AMI)... ");
        Console.Out.Flush();
        {
            D1 d11 = new D1();
            d11.sb = "D1.sb(1)";
            d11.pb = d11;
            d11.sd1 = "D1.sd1(1)";
            
            D3 d3 = new D3();
            d3.sb = "D3.sb";
            d3.pb = d3;
            d3.sd3 = "D3.sd3";
            d3.pd3 = d11;
            
            D1 d12 = new D1();
            d12.sb = "D1.sb(2)";
            d12.pb = d12;
            d12.sd1 = "D1.sd1(2)";
            d12.pd1 = d11;
            
            AMI_Test_returnTest3I cb = new AMI_Test_returnTest3I();
            testPrx.returnTest3_async(cb, d3, d12);
            test(cb.check());
            B r = cb.r;
            
            test(r != null);
            test(r.ice_id(null).Equals("::B"));
            test(r.sb.Equals("D3.sb"));
            test(r.pb == r);
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("sequence slicing... ");
        Console.Out.Flush();
        {
            try
            {
                SS ss;
                {
                    B ss1b = new B();
                    ss1b.sb = "B.sb";
                    ss1b.pb = ss1b;
                    
                    D1 ss1d1 = new D1();
                    ss1d1.sb = "D1.sb";
                    ss1d1.sd1 = "D1.sd1";
                    ss1d1.pb = ss1b;
                    
                    D3 ss1d3 = new D3();
                    ss1d3.sb = "D3.sb";
                    ss1d3.sd3 = "D3.sd3";
                    ss1d3.pb = ss1b;
                    
                    B ss2b = new B();
                    ss2b.sb = "B.sb";
                    ss2b.pb = ss1b;
                    
                    D1 ss2d1 = new D1();
                    ss2d1.sb = "D1.sb";
                    ss2d1.sd1 = "D1.sd1";
                    ss2d1.pb = ss2b;
                    
                    D3 ss2d3 = new D3();
                    ss2d3.sb = "D3.sb";
                    ss2d3.sd3 = "D3.sd3";
                    ss2d3.pb = ss2b;
                    
                    ss1d1.pd1 = ss2b;
                    ss1d3.pd3 = ss2d1;
                    
                    ss2d1.pd1 = ss1d3;
                    ss2d3.pd3 = ss1d1;
                    
                    SS1 ss1 = new SS1();
                    ss1.s = new BSeq(3);
                    ss1.s.Add(ss1b);
                    ss1.s.Add(ss1d1);
                    ss1.s.Add(ss1d3);
                    
                    SS2 ss2 = new SS2();
                    ss2.s = new BSeq(3);
                    ss2.s.Add(ss2b);
                    ss2.s.Add(ss2d1);
                    ss2.s.Add(ss2d3);
                    
                    ss = testPrx.sequenceTest(ss1, ss2);
                }
                
                test(ss.c1 != null);
                B ss1b2 = ss.c1.s[0];
                B ss1d2 = ss.c1.s[1];
                test(ss.c2 != null);
                B ss1d4 = ss.c1.s[2];
                
                test(ss.c2 != null);
                B ss2b2 = ss.c2.s[0];
                B ss2d2 = ss.c2.s[1];
                B ss2d4 = ss.c2.s[2];
                
                test(ss1b2.pb == ss1b2);
                test(ss1d2.pb == ss1b2);
                test(ss1d4.pb == ss1b2);
                
                test(ss2b2.pb == ss1b2);
                test(ss2d2.pb == ss2b2);
                test(ss2d4.pb == ss2b2);
                
                test(ss1b2.ice_id(null).Equals("::B"));
                test(ss1d2.ice_id(null).Equals("::D1"));
                test(ss1d4.ice_id(null).Equals("::B"));
                
                test(ss2b2.ice_id(null).Equals("::B"));
                test(ss2d2.ice_id(null).Equals("::D1"));
                test(ss2d4.ice_id(null).Equals("::B"));
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("sequence slicing (AMI)... ");
        Console.Out.Flush();
        {
            SS ss;
            {
                B ss1b = new B();
                ss1b.sb = "B.sb";
                ss1b.pb = ss1b;
                
                D1 ss1d1 = new D1();
                ss1d1.sb = "D1.sb";
                ss1d1.sd1 = "D1.sd1";
                ss1d1.pb = ss1b;
                
                D3 ss1d3 = new D3();
                ss1d3.sb = "D3.sb";
                ss1d3.sd3 = "D3.sd3";
                ss1d3.pb = ss1b;
                
                B ss2b = new B();
                ss2b.sb = "B.sb";
                ss2b.pb = ss1b;
                
                D1 ss2d1 = new D1();
                ss2d1.sb = "D1.sb";
                ss2d1.sd1 = "D1.sd1";
                ss2d1.pb = ss2b;
                
                D3 ss2d3 = new D3();
                ss2d3.sb = "D3.sb";
                ss2d3.sd3 = "D3.sd3";
                ss2d3.pb = ss2b;
                
                ss1d1.pd1 = ss2b;
                ss1d3.pd3 = ss2d1;
                
                ss2d1.pd1 = ss1d3;
                ss2d3.pd3 = ss1d1;
                
                SS1 ss1 = new SS1();
                ss1.s = new B[3];
                ss1.s[0] = ss1b;
                ss1.s[1] = ss1d1;
                ss1.s[2] = ss1d3;
                
                SS2 ss2 = new SS2();
                ss2.s = new B[3];
                ss2.s[0] = ss2b;
                ss2.s[1] = ss2d1;
                ss2.s[2] = ss2d3;
                
                AMI_Test_sequenceTestI cb = new AMI_Test_sequenceTestI();
                testPrx.sequenceTest_async(cb, ss1, ss2);
                test(cb.check());
                ss = cb.r;
            }
            test(ss.c1 != null);
            B ss1b3 = ss.c1.s[0];
            B ss1d5 = ss.c1.s[1];
            test(ss.c2 != null);
            B ss1d6 = ss.c1.s[2];
            
            test(ss.c2 != null);
            B ss2b3 = ss.c2.s[0];
            B ss2d5 = ss.c2.s[1];
            B ss2d6 = ss.c2.s[2];
            
            test(ss1b3.pb == ss1b3);
            test(ss1d6.pb == ss1b3);
            test(ss1d6.pb == ss1b3);
            
            test(ss2b3.pb == ss1b3);
            test(ss2d6.pb == ss2b3);
            test(ss2d6.pb == ss2b3);
            
            test(ss1b3.ice_id(null).Equals("::B"));
            test(ss1d6.ice_id(null).Equals("::D1"));
            test(ss1d6.ice_id(null).Equals("::B"));
            
            test(ss2b3.ice_id(null).Equals("::B"));
            test(ss2d6.ice_id(null).Equals("::D1"));
            test(ss2d6.ice_id(null).Equals("::B"));
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("dictionary slicing... ");
        Console.Out.Flush();
        {
            try
            {
                BDict bin = new BDict();
                BDict bout;
                BDict ret;
                int i;
                for (i = 0; i < 10; ++i)
                {
                    string s = "D1." + i.ToString();
                    D1 d1 = new D1();
                    d1.sb = s;
                    d1.pb = d1;
                    d1.sd1 = s;
                    bin[i] = d1;
                }
                
                ret = testPrx.dictionaryTest(bin, out bout);
                
                test(bout.Count == 10);
                for (i = 0; i < 10; ++i)
                {
                    B b = bout[i * 10];
                    test(b != null);
                    string s = "D1." + i.ToString();
                    test(b.sb.Equals(s));
                    test(b.pb != null);
                    test(b.pb != b);
                    test(b.pb.sb.Equals(s));
                    test(b.pb.pb == b.pb);
                }
                
                test(ret.Count == 10);
                for (i = 0; i < 10; ++i)
                {
                    B b = ret[i * 20];
                    test(b != null);
                    string s = "D1." + (i * 20).ToString();
                    test(b.sb.Equals(s));
                    test(b.pb == (i == 0 ? (B)null : ret[(i - 1) * 20]));
                    D1 d1 = (D1) b;
                    test(d1 != null);
                    test(d1.sd1.Equals(s));
                    test(d1.pd1 == d1);
                }
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("dictionary slicing (AMI)... ");
        Console.Out.Flush();
        {
            //UPGRADE_TODO: Interface java.util was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1095"'
            //UPGRADE_TODO: Field java.util was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1095"'
            java.util.Map bin = new java.util.HashMap();
            //UPGRADE_TODO: Interface java.util was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1095"'
            java.util.Map bout;
            //UPGRADE_TODO: Interface java.util was not converted. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1095"'
            java.util.Map r;
            int i;
            for (i = 0; i < 10; ++i)
            {
                string s = "D1." + i.ToString();
                D1 d1 = new D1();
                d1.sb = s;
                d1.pb = d1;
                d1.sd1 = s;
                bin.put(i, d1);
            }
            
            AMI_Test_dictionaryTestI cb = new AMI_Test_dictionaryTestI();
            testPrx.dictionaryTest_async(cb, bin);
            test(cb.check());
            bout = cb.bout;
            r = cb.r;
            
            test(bout.size() == 10);
            for (i = 0; i < 10; ++i)
            {
                B b = (B) bout.get(i * 10);
                test(b != null);
                string s = "D1." + i.ToString();
                test(b.sb.Equals(s));
                test(b.pb != null);
                test(b.pb != b);
                test(b.pb.sb.Equals(s));
                test(b.pb.pb == b.pb);
            }
            
            test(r.size() == 10);
            for (i = 0; i < 10; ++i)
            {
                B b = (B) r.get(i * 20);
                test(b != null);
                string s = "D1." + (i * 20).ToString();
                test(b.sb.Equals(s));
                test(b.pb == (i == 0?(B) null:(B) r.get((i - 1) * 20)));
                D1 d1 = (D1) b;
                test(d1 != null);
                test(d1.sd1.Equals(s));
                test(d1.pd1 == d1);
            }
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("base exception thrown as base exception... ");
        Console.Out.Flush();
        {
            try
            {
                testPrx.throwBaseAsBase();
                test(false);
            }
            catch(BaseException e)
            {
                test(e.GetType().FullName.Equals("BaseException"));
                test(e.sbe.Equals("sbe"));
                test(e.pb != null);
                test(e.pb.sb.Equals("sb"));
                test(e.pb.pb == e.pb);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("base exception thrown as base exception (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_throwBaseAsBaseI cb = new AMI_Test_throwBaseAsBaseI();
            testPrx.throwBaseAsBase_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("derived exception thrown as base exception... ");
        Console.Out.Flush();
        {
            try
            {
                testPrx.throwDerivedAsBase();
                test(false);
            }
            catch(DerivedException e)
            {
                test(e.GetType().FullName.Equals("DerivedException"));
                test(e.sbe.Equals("sbe"));
                test(e.pb != null);
                test(e.pb.sb.Equals("sb1"));
                test(e.pb.pb == e.pb);
                test(e.sde.Equals("sde1"));
                test(e.pd1 != null);
                test(e.pd1.sb.Equals("sb2"));
                test(e.pd1.pb == e.pd1);
                test(e.pd1.sd1.Equals("sd2"));
                test(e.pd1.pd1 == e.pd1);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("derived exception thrown as base exception (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_throwDerivedAsBaseI cb = new AMI_Test_throwDerivedAsBaseI();
            testPrx.throwDerivedAsBase_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("derived exception thrown as derived exception... ");
        Console.Out.Flush();
        {
            try
            {
                testPrx.throwDerivedAsDerived();
                test(false);
            }
            catch(DerivedException e)
            {
                test(e.GetType().FullName.Equals("DerivedException"));
                test(e.sbe.Equals("sbe"));
                test(e.pb != null);
                test(e.pb.sb.Equals("sb1"));
                test(e.pb.pb == e.pb);
                test(e.sde.Equals("sde1"));
                test(e.pd1 != null);
                test(e.pd1.sb.Equals("sb2"));
                test(e.pd1.pb == e.pd1);
                test(e.pd1.sd1.Equals("sd2"));
                test(e.pd1.pd1 == e.pd1);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("derived exception thrown as derived exception (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_throwDerivedAsDerivedI cb = new AMI_Test_throwDerivedAsDerivedI();
            testPrx.throwDerivedAsDerived_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("unknown derived exception thrown as base exception... ");
        Console.Out.Flush();
        {
            try
            {
                testPrx.throwUnknownDerivedAsBase();
                test(false);
            }
            catch(BaseException e)
            {
                test(e.GetType().FullName.Equals("BaseException"));
                test(e.sbe.Equals("sbe"));
                test(e.pb != null);
                test(e.pb.sb.Equals("sb d2"));
                test(e.pb.pb == e.pb);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("unknown derived exception thrown as base exception (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_throwUnknownDerivedAsBaseI cb = new AMI_Test_throwUnknownDerivedAsBaseI();
            testPrx.throwUnknownDerivedAsBase_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        Console.Out.Write("forward-declared class... ");
        Console.Out.Flush();
        {
            try
            {
                Forward f;
                testPrx.useForward(out f);
                test(f != null);
            }
            catch(Exception)
            {
                test(false);
            }
        }
        Console.Out.WriteLine("ok");
        
	/*
        Console.Out.Write("forward-declared class (AMI)... ");
        Console.Out.Flush();
        {
            AMI_Test_useForwardI cb = new AMI_Test_useForwardI();
            testPrx.useForward_async(cb);
            test(cb.check());
        }
        Console.Out.WriteLine("ok");
	*/
        
        return testPrx;
    }
}
