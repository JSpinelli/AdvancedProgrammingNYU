using UnityEngine;
public static class Services {

	private static GameController _gameController ;
    public static GameController GameController
    {
        get
        {
            Debug.Assert(_gameController != null);
            return _gameController;
        }
        set => _gameController = value;
    }

	public static Player player1;
	public static Player player2;

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