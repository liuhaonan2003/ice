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


namespace IceInternal
{

    using System.Collections;

    public sealed class LocatorManager
    {
	internal LocatorManager()
	{
	    _table = new Hashtable();
	    _locatorTables = new Hashtable();
	}
	
	internal void destroy()
	{
	    lock(this)
	    {
		foreach(LocatorInfo info in _table.Values)
		{
		    info.destroy();
		}
		_table.Clear();
		_locatorTables.Clear();
	    }
	}
	
	//
	// Returns locator info for a given locator. Automatically creates
	// the locator info if it doesn't exist yet.
	//
	public LocatorInfo get(Ice.LocatorPrx loc)
	{
	    if(loc == null)
	    {
		return null;
	    }
	    
	    //
	    // The locator can't be located.
	    //
	    Ice.LocatorPrx locator = Ice.LocatorPrxHelper.uncheckedCast(loc.ice_locator(null));
	    
	    //
	    // TODO: reap unused locator info objects?
	    //
	    
	    lock(this)
	    {
		LocatorInfo info = (LocatorInfo)_table[locator];
		if(info == null)
		{
		    //
		    // Rely on locator identity for the adapter table. We want to
		    // have only one table per locator (not one per locator
		    // proxy).
		    //
		    LocatorTable table = (LocatorTable)_locatorTables[locator.ice_getIdentity()];
		    if(table == null)
		    {
			table = new LocatorTable();
			_locatorTables[locator.ice_getIdentity()] = table;
		    }
		    
		    info = new LocatorInfo(locator, table);
		    _table[locator] = info;
		}
		
		return info;
	    }
	}
	
	private Hashtable _table;
	private Hashtable _locatorTables;
    }

}
