using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager {
	
    //Hashmap of all the Events
	private Dictionary<Type, AGPEvent.Handler> _registeredHandlers = new Dictionary<Type, AGPEvent.Handler>();

	public void Register<T>(AGPEvent.Handler handler) where T : AGPEvent 
	{
		var type = typeof(T);
		if (_registeredHandlers.ContainsKey(type)) 
		{
			if (!IsEventHandlerRegistered(type, handler))
				_registeredHandlers[type] += handler;         
		} 
		else 
		{
			_registeredHandlers.Add(type, handler);         
		}     
	} 

	public void Unregister<T>(AGPEvent.Handler handler) where T : AGPEvent 
	{         
		var type = typeof(T);
		if (!_registeredHandlers.TryGetValue(type, out var handlers)) return;
		
		handlers -= handler;  
		
		if (handlers == null) 
		{                 
			_registeredHandlers.Remove(type);             
		} 
		else
		{
			_registeredHandlers[type] = handlers;             
		}
	}      
		
	public void Fire(AGPEvent e) 
	{       
		var type = e.GetType();

        //Find all the handlers of the triggered event
		if (_registeredHandlers.TryGetValue(type, out var handlers)) 
		{   
            //Pass the vent to the handlers nad let them figure it out         
			handlers(e);
		}     
	} 

    //Check if a handler is already registered
	public bool IsEventHandlerRegistered (Type typeIn, Delegate prospectiveHandler)
	{
		return _registeredHandlers[typeIn].GetInvocationList().Any(existingHandler => existingHandler == prospectiveHandler);
	}
}

public abstract class AGPEvent 
{
	public readonly float creationTime;

	public AGPEvent ()
	{
		creationTime = Time.time;
	}

	public delegate void Handler (AGPEvent e);
}

public class GoalScored : AGPEvent
{
	public readonly bool team1;
	
	public GoalScored(string goalOwner)
	{
		this.team1 = goalOwner != "Player 1";
	}
}

public class GameStart : AGPEvent
{}

public class GameEnd : AGPEvent
{
	public readonly bool team1won;
	
	public GameEnd(bool team1won)
	{
		this.team1won = team1won;
	}
}

public class TimeOut : AGPEvent
{}
