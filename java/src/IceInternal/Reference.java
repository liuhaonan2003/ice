// **********************************************************************
//
// Copyright (c) 2003-2004 ZeroC, Inc. All rights reserved.
//
// This copy of Ice is licensed to you under the terms described in the
// ICE_LICENSE file included in this distribution.
//
// **********************************************************************

package IceInternal;

public final class Reference
{
    public final static int ModeTwoway = 0;
    public final static int ModeOneway = 1;
    public final static int ModeBatchOneway = 2;
    public final static int ModeDatagram = 3;
    public final static int ModeBatchDatagram = 4;
    public final static int ModeLast = ModeBatchDatagram;

    public int
    hashCode()
    {
        return hashValue;
    }

    public boolean
    equals(java.lang.Object obj)
    {
        Reference r = (Reference)obj;

        if(r == null)
        {
            return false;
        }

        if(this == r)
        {
            return true;
        }

        if(!identity.category.equals(r.identity.category))
        {
            return false;
        }

        if(!identity.name.equals(r.identity.name))
        {
            return false;
        }

	if(!context.equals(r.context))
	{
	    return false;
	}

        if(!facet.equals(r.facet))
        {
            return false;
        }

        if(mode != r.mode)
        {
            return false;
        }

        if(secure != r.secure)
        {
            return false;
        }

	if(!adapterId.equals(r.adapterId))
	{
	    return false;
	}
	
        if(!compare(endpoints, r.endpoints))
        {
            return false;
        }

        if(routerInfo != r.routerInfo)
        {
            return false;
        }

        if(routerInfo != null && r.routerInfo != null &&
	   !routerInfo.equals(r.routerInfo))
        {
            return false;
        }

        if(locatorInfo != r.locatorInfo)
        {
            return false;
        }

        if(locatorInfo != null && r.locatorInfo != null &&
	   !locatorInfo.equals(r.locatorInfo))
        {
            return false;
        }

        if(fixedConnections != null && r.fixedConnections != null &&
           !java.util.Arrays.equals(fixedConnections, r.fixedConnections))
        {
            return false;
        }

        if(collocationOptimization != r.collocationOptimization)
        {
            return false;
        }
	
        return true;
    }

    //
    // Marshal the reference.
    //
    public void
    streamWrite(BasicStream s)
    {
        //
        // Don't write the identity here. Operations calling streamWrite
        // write the identity.
        //

        //
        // For compatibility with the old FacetPath.
        //
        if(facet.length() == 0)
        {
            s.writeStringSeq(null);
        }
        else
        {
            String[] facetPath = { facet };
            s.writeStringSeq(facetPath);
        }

        s.writeByte((byte)mode);

        s.writeBool(secure);

	s.writeSize(endpoints.length);

	if(endpoints.length > 0)
	{
	    assert(adapterId.equals(""));
	    
            for(int i = 0; i < endpoints.length; i++)
            {
                endpoints[i].streamWrite(s);
            }
	}
        else
        {
	    s.writeString(adapterId);
        }
    }

    //
    // Convert the reference to its string form.
    //
    public String
    toString()
    {
        StringBuffer s = new StringBuffer();

        //
        // If the encoded identity string contains characters which
        // the reference parser uses as separators, then we enclose
        // the identity string in quotes.
        //
        String id = Ice.Util.identityToString(identity);
        if(IceUtil.StringUtil.findFirstOf(id, " \t\n\r:@") != -1)
        {
            s.append('"');
            s.append(id);
            s.append('"');
        }
        else
        {
            s.append(id);
        }

        if(facet.length() > 0)
        {
            //
            // If the encoded facet string contains characters which
            // the reference parser uses as separators, then we enclose
            // the facet string in quotes.
            //
            s.append(" -f ");
            String fs = IceUtil.StringUtil.escapeString(facet, "");
            if(IceUtil.StringUtil.findFirstOf(fs, " \t\n\r:@") != -1)
            {
                s.append('"');
                s.append(fs);
                s.append('"');
            }
            else
            {
                s.append(fs);
            }
        }

        switch(mode)
        {
            case ModeTwoway:
            {
                s.append(" -t");
                break;
            }

            case ModeOneway:
            {
                s.append(" -o");
                break;
            }

            case ModeBatchOneway:
            {
                s.append(" -O");
                break;
            }

            case ModeDatagram:
            {
                s.append(" -d");
                break;
            }

            case ModeBatchDatagram:
            {
                s.append(" -D");
                break;
            }
        }

        if(secure)
        {
            s.append(" -s");
        }

	if(endpoints.length > 0)
	{
	    assert(adapterId.equals(""));

	    for(int i = 0; i < endpoints.length; i++)
	    {
		String endp = endpoints[i].toString();
		if(endp != null && endp.length() > 0)
		{
		    s.append(':');
		    s.append(endp);
		}
	    }
	}
        else
        {
            String a = IceUtil.StringUtil.escapeString(adapterId, null);
            //
            // If the encoded adapter id string contains characters which
            // the reference parser uses as separators, then we enclose
            // the adapter id string in quotes.
            //
            s.append(" @ ");
            if(IceUtil.StringUtil.findFirstOf(a, " \t\n\r") != -1)
            {
                s.append('"');
                s.append(a);
                s.append('"');
            }
            else
            {
                s.append(a);
            }
        }

        return s.toString();
    }

    //
    // All members are treated as const, because References are immutable.
    //
    final public Instance instance;
    final public Ice.Identity identity;
    final public java.util.Map context;
    final public String facet;
    final public int mode;
    final public boolean secure;
    final public String adapterId;
    final public Endpoint[] endpoints;
    final public RouterInfo routerInfo; // Null if no router is used.
    final public LocatorInfo locatorInfo; // Null if no locator is used.
    final public Ice.ConnectionI[] fixedConnections; // For using fixed connections, otherwise empty.
    final public boolean collocationOptimization;
    final public int hashValue;

    //
    // Get a new reference, based on the existing one, overwriting
    // certain values.
    //
    public Reference
    changeIdentity(Ice.Identity newIdentity)
    {
        if(newIdentity.equals(identity))
        {
            return this;
        }
        else
        {
            return instance.referenceFactory().create(newIdentity, context, facet, mode, secure, adapterId,
 						      endpoints, routerInfo, locatorInfo, fixedConnections,
						      collocationOptimization);
        }
    }

    public Reference
    changeContext(java.util.Map newContext)
    {
        if(newContext.equals(context))
        {
            return this;
        }
        else
        {
            return instance.referenceFactory().create(identity, newContext, facet, mode, secure, adapterId,
 						      endpoints, routerInfo, locatorInfo, fixedConnections,
						      collocationOptimization);
        }
    }

    public Reference
    changeFacet(String newFacet)
    {
        if(facet.equals(newFacet))
        {
            return this;
        }
        else
        {
            return instance.referenceFactory().create(identity, context, newFacet, mode, secure, adapterId,
						      endpoints, routerInfo, locatorInfo, fixedConnections,
						      collocationOptimization);
        }
    }

    public Reference
    changeTimeout(int newTimeout)
    {
        //
        // We change the timeout settings in all endpoints.
        //
        Endpoint[] newEndpoints = new Endpoint[endpoints.length];
        for(int i = 0; i < endpoints.length; i++)
        {
            newEndpoints[i] = endpoints[i].timeout(newTimeout);
        }

        return instance.referenceFactory().create(identity, context, facet, mode, secure, adapterId, 
						  newEndpoints, routerInfo, locatorInfo, fixedConnections,
						  collocationOptimization);
    }

    public Reference
    changeMode(int newMode)
    {
        if(newMode == mode)
        {
            return this;
        }
        else
        {
            return instance.referenceFactory().create(identity, context, facet, newMode, secure, adapterId,
						      endpoints, routerInfo, locatorInfo, fixedConnections,
						      collocationOptimization);
        }
    }

    public Reference
    changeSecure(boolean newSecure)
    {
        if(newSecure == secure)
        {
            return this;
        }
        else
        {
            return instance.referenceFactory().create(identity, context, facet, mode, newSecure, adapterId,
						      endpoints, routerInfo, locatorInfo, fixedConnections,
						      collocationOptimization);
        }
    }

    public Reference
    changeCompress(boolean newCompress)
    {
        //
        // We change the compress settings in all endpoints.
        //
        Endpoint[] newEndpoints = new Endpoint[endpoints.length];
        for(int i = 0; i < endpoints.length; i++)
        {
            newEndpoints[i] = endpoints[i].compress(newCompress);
        }

        return instance.referenceFactory().create(identity, context, facet, mode, secure, adapterId, 
						  newEndpoints, routerInfo, locatorInfo, fixedConnections,
						  collocationOptimization);
    }

    public Reference
    changeAdapterId(String newAdapterId)
    {
	if(adapterId.equals(newAdapterId))
	{
	    return this;
	}
	else
	{
	    return instance.referenceFactory().create(identity, context, facet, mode, secure, newAdapterId,
						      endpoints, routerInfo, locatorInfo, fixedConnections,
						      collocationOptimization);
	}
    }

    public Reference
    changeEndpoints(Endpoint[] newEndpoints)
    {
        if(compare(newEndpoints, endpoints))
        {
            return this;
        }
        else
        {
            return instance.referenceFactory().create(identity, context, facet, mode, secure, adapterId,
						      newEndpoints, routerInfo, locatorInfo, fixedConnections,
						      collocationOptimization);
        }
    }

    public Reference
    changeRouter(Ice.RouterPrx newRouter)
    {
        RouterInfo newRouterInfo = instance.routerManager().get(newRouter);

        if((routerInfo == newRouterInfo) ||
            (routerInfo != null && newRouterInfo != null && newRouterInfo.equals(routerInfo)))
        {
            return this;
        }
        else
        {
            return instance.referenceFactory().create(identity, context, facet, mode, secure, adapterId,
						      endpoints, newRouterInfo, locatorInfo, fixedConnections,
						      collocationOptimization);
        }
    }

    public Reference
    changeLocator(Ice.LocatorPrx newLocator)
    {
        LocatorInfo newLocatorInfo = instance.locatorManager().get(newLocator);

        if((locatorInfo == newLocatorInfo) ||
            (locatorInfo != null && newLocatorInfo != null && newLocatorInfo.equals(locatorInfo)))
        {
            return this;
        }
        else
        {
            return instance.referenceFactory().create(identity, context, facet, mode, secure, adapterId,
						      endpoints, routerInfo, newLocatorInfo, fixedConnections,
						      collocationOptimization);
        }
    }

    public Reference
    changeCollocationOptimization(boolean newCollocationOptimization)
    {
        if(newCollocationOptimization == collocationOptimization)
        {
            return this;
        }
        else
        {
            return instance.referenceFactory().create(identity, context, facet, mode, secure, adapterId,
						      endpoints, routerInfo, locatorInfo, fixedConnections,
						      newCollocationOptimization);
        }
    }

    public Reference
    changeDefault()
    {
        RouterInfo routerInfo = instance.routerManager().get(instance.referenceFactory().getDefaultRouter());
        LocatorInfo locatorInfo = instance.locatorManager().get(instance.referenceFactory().getDefaultLocator());

        return instance.referenceFactory().create(identity, context, "", ModeTwoway, false, adapterId,
						  endpoints, routerInfo, locatorInfo, new Ice.ConnectionI[0], true);
    }

    //
    // Get a suitable connection for this reference.
    //
    public Ice.ConnectionI
    getConnection()
    {
	Ice.ConnectionI connection;

        //
        // If we have a fixed connection, we return such fixed connection.
        //
        if(fixedConnections.length > 0)
        {
            Ice.ConnectionI[] filteredConns = filterConnections(fixedConnections);
            if(filteredConns.length == 0)
            {
                Ice.NoEndpointException e = new Ice.NoEndpointException();
		e.proxy = toString();
		throw e;
            }

            connection = filteredConns[0];
        }
        else
        {
	    while(true)
	    {
		Ice.BooleanHolder cached = new Ice.BooleanHolder();
		cached.value = false;
		Endpoint[] endpts = null;

		if(routerInfo != null)
		{
		    //
		    // If we route, we send everything to the router's client
		    // proxy endpoints.
		    //
		    Ice.ObjectPrx clientProxy = routerInfo.getClientProxy();
		    endpts = ((Ice.ObjectPrxHelperBase)clientProxy).__reference().endpoints;
		}
		else if(endpoints.length > 0)
		{
		    endpts = endpoints;
		}
		else if(locatorInfo != null)
		{
		    endpts = locatorInfo.getEndpoints(this, cached);
		}

		Endpoint[] filteredEndpts = null;
                if(endpts != null)
                {
                    filteredEndpts = filterEndpoints(endpts);
                }
		if(filteredEndpts == null || filteredEndpts.length == 0)
		{
		    Ice.NoEndpointException e = new Ice.NoEndpointException();
		    e.proxy = toString();
		    throw e;
		}

		try
		{
		    OutgoingConnectionFactory factory = instance.outgoingConnectionFactory();
		    connection = factory.create(filteredEndpts);
		    assert(connection != null);
		}
		catch(Ice.LocalException ex)
		{
		    if(routerInfo == null && endpoints.length == 0)
		    {
			assert(locatorInfo != null);
			locatorInfo.clearCache(this);
			
			if(cached.value)
			{
			    TraceLevels traceLevels = instance.traceLevels();
			    Ice.Logger logger = instance.logger();
			    
			    if(traceLevels.retry >= 2)
			    {
				String s = "connection to cached endpoints failed\n" +
				    "removing endpoints from cache and trying one more time\n" + ex;
				logger.trace(traceLevels.retryCat, s);
			    }
			    
			    continue;
			}
		    }

		    throw ex;
		}   

		break;
	    }

            //
            // If we have a router, set the object adapter for this
            // router (if any) to the new connection, so that
            // callbacks from the router can be received over this new
            // connection.
            //
            if(routerInfo != null)
            {
                connection.setAdapter(routerInfo.getAdapter());
            }
        }

	assert(connection != null);
	return connection;
    }

    //
    // Filter endpoints based on criteria from this reference.
    //
    public Endpoint[]
    filterEndpoints(Endpoint[] allEndpoints)
    {
        java.util.ArrayList endpoints = new java.util.ArrayList();

        //
        // Filter out unknown endpoints.
        //
        for(int i = 0; i < allEndpoints.length; i++)
        {
            if(!allEndpoints[i].unknown())
            {
                endpoints.add(allEndpoints[i]);
            }
        }

        switch(mode)
        {
            case Reference.ModeTwoway:
            case Reference.ModeOneway:
            case Reference.ModeBatchOneway:
            {
                //
                // Filter out datagram endpoints.
                //
                java.util.Iterator i = endpoints.iterator();
                while(i.hasNext())
                {
                    Endpoint endpoint = (Endpoint)i.next();
                    if(endpoint.datagram())
                    {
                        i.remove();
                    }
                }
                break;
            }

            case Reference.ModeDatagram:
            case Reference.ModeBatchDatagram:
            {
                //
                // Filter out non-datagram endpoints.
                //
                java.util.Iterator i = endpoints.iterator();
                while(i.hasNext())
                {
                    Endpoint endpoint = (Endpoint)i.next();
                    if(!endpoint.datagram())
                    {
                        i.remove();
                    }
                }
                break;
            }
        }

        //
        // Randomize the order of endpoints.
        //
        java.util.Collections.shuffle(endpoints);

        //
        // If a secure connection is requested, remove all non-secure
        // endpoints. Otherwise make non-secure endpoints preferred over
        // secure endpoints by partitioning the endpoint vector, so that
        // non-secure endpoints come first.
        //
        if(secure)
        {
            java.util.Iterator i = endpoints.iterator();
            while(i.hasNext())
            {
                Endpoint endpoint = (Endpoint)i.next();
                if(!endpoint.secure())
                {
                    i.remove();
                }
            }
        }
        else
        {
            java.util.Collections.sort(endpoints, _endpointComparator);
        }

        Endpoint[] arr = new Endpoint[endpoints.size()];
        endpoints.toArray(arr);
        return arr;
    }

    static class EndpointComparator implements java.util.Comparator
    {
	public int
	compare(java.lang.Object l, java.lang.Object r)
	{
	    IceInternal.Endpoint le = (IceInternal.Endpoint)l;
	    IceInternal.Endpoint re = (IceInternal.Endpoint)r;
	    boolean ls = le.secure();
	    boolean rs = re.secure();
	    if((ls && rs) || (!ls && !rs))
	    {
		return 0;
	    }
	    else if(!ls && rs)
	    {
		return -1;
	    }
	    else
	    {
		return 1;
	    }
	}
    }
    
    private static EndpointComparator _endpointComparator = new EndpointComparator();

    //
    // Filter connections based on criteria from this reference.
    //
    public Ice.ConnectionI[]
    filterConnections(Ice.ConnectionI[] allConnections)
    {
        java.util.ArrayList connections = new java.util.ArrayList(allConnections.length);

        switch(mode)
        {
            case Reference.ModeTwoway:
            case Reference.ModeOneway:
            case Reference.ModeBatchOneway:
            {
                //
                // Filter out datagram connections.
                //
                for(int i = 0; i < allConnections.length; ++i)
                {
                    if(!allConnections[i].endpoint().datagram())
                    {
                        connections.add(allConnections[i]);
                    }
                }

                break;
            }

            case Reference.ModeDatagram:
            case Reference.ModeBatchDatagram:
            {
                //
                // Filter out non-datagram connections.
                //
                for(int i = 0; i < allConnections.length; i++)
                {
                    if(allConnections[i].endpoint().datagram())
                    {
                        connections.add(allConnections[i]);
                    }
                }

                break;
            }
        }

        //
        // Randomize the order of connections.
        //
        java.util.Collections.shuffle(connections);

        //
        // If a secure connection is requested, remove all non-secure
        // endpoints. Otherwise make non-secure endpoints preferred over
        // secure endpoints by partitioning the endpoint vector, so that
        // non-secure endpoints come first.
        //
        if(secure)
        {
            java.util.Iterator i = connections.iterator();
            while(i.hasNext())
            {
                Ice.ConnectionI connection = (Ice.ConnectionI)i.next();
                if(!connection.endpoint().secure())
                {
                    i.remove();
                }
            }
        }
        else
        {
            java.util.Collections.sort(connections, _connectionComparator);
        }

        Ice.ConnectionI[] arr = new Ice.ConnectionI[connections.size()];
        connections.toArray(arr);
        return arr;
    }

    static class ConnectionComparator implements java.util.Comparator
    {
	public int
	compare(java.lang.Object l, java.lang.Object r)
	{
	    Ice.ConnectionI lc = (Ice.ConnectionI)l;
	    Ice.ConnectionI rc = (Ice.ConnectionI)r;
	    boolean ls = lc.endpoint().secure();
	    boolean rs = rc.endpoint().secure();
	    if((ls && rs) || (!ls && !rs))
	    {
		return 0;
	    }
	    else if(!ls && rs)
	    {
		return -1;
	    }
	    else
	    {
		return 1;
	    }
	}
    }
    
    private static ConnectionComparator _connectionComparator = new ConnectionComparator();

    //
    // Only for use by ReferenceFactory
    //
    Reference(Instance inst,
              Ice.Identity ident,
	      java.util.Map ctx,
              String fac,
              int md,
              boolean sec,
	      String adptId,
              Endpoint[] endpts,
              RouterInfo rtrInfo,
	      LocatorInfo locInfo,
              Ice.ConnectionI[] fixedConns,
	      boolean collocationOpt)
    {
        //
        // Validate string arguments.
        //
        assert(ident.name != null);
        assert(ident.category != null);
        assert(fac != null);
        assert(adptId != null);

	//
	// It's either adapter id or endpoints, it can't be both.
	//
	assert(!(!adptId.equals("") && !(endpts.length == 0)));

        instance = inst;
        identity = ident;
	context = ctx;
        facet = fac;
        mode = md;
        secure = sec;
	adapterId = adptId;
        endpoints = endpts;
        routerInfo = rtrInfo;
        locatorInfo = locInfo;
        fixedConnections = fixedConns;
	collocationOptimization = collocationOpt;

        int h = 0;

        int sz = identity.name.length();
        for(int i = 0; i < sz; i++)
        {   
            h = 5 * h + (int)identity.name.charAt(i);
        }

        sz = identity.category.length();
        for(int i = 0; i < sz; i++)
        {   
            h = 5 * h + (int)identity.category.charAt(i);
        }

	h = 5 * h + context.entrySet().hashCode();

        sz = facet.length();
        for(int i = 0; i < sz; i++)
        {   
            h = 5 * h + (int)facet.charAt(i);
        }

        h = 5 * h + mode;

        h = 5 * h + (secure ? 1 : 0);

        //
        // TODO: Should we also take the endpoints into account for hash
        // calculation? Perhaps not, the code above should be good enough
        // for a good hash value.
        //

	hashValue = h;
    }

    private boolean
    compare(Endpoint[] arr1, Endpoint[] arr2)
    {
        if(arr1 == arr2)
        {
            return true;
        }

        if(arr1.length == arr2.length)
        {
            for(int i = 0; i < arr1.length; i++)
            {
                if(!arr1[i].equals(arr2[i]))
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }
}
