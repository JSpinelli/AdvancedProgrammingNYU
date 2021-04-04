using UnityEngine;
public static class Services {

	private static GameManager _gameManager ;
    public static GameManager gameManager
    {
        get
        {
            Debug.Assert(_gameManager != null);
            return _gameManager;
        }
        set => _gameManager = value;
    }
    	
    private static AudioSource _audioSource;
    public static AudioSource audioSource
    {
        get
        {
            Debug.Assert(_audioSource != null);
            return _audioSource;
        }
        set => _audioSource = value;
    }
    
    private static InputManager _input;
    public static InputManager Input
    {
        get
        {
            Debug.Assert(_input != null);
            return _input;
        }
        set => _input = value;
    }
    
    private static AIController _ai;
    public static AIController AIManager
    {
        get
        {
            Debug.Assert(_ai != null);
            return _ai;
        }
        set => _ai = value;
    }
    
    private static RefereeBehaviour _referee;
    public static RefereeBehaviour RefereeBehaviour
    {
        get
        {
            Debug.Assert(_referee != null);
            return _referee;
        }
        set => _referee = value;
    }
    
    private static PlayerControlled[] _players;
    public static PlayerControlled[] Players
    {
        get
        {
            Debug.Assert(_players != null);
            return _players;
        }
        set => _players = value;
    }
    
	private static EventManager _eventManager;
    public static EventManager EventManager
    {
        get
        {
            Debug.Assert(_eventManager != null);
            return _eventManager;
        }
        set => _eventManager = value;
    }
}