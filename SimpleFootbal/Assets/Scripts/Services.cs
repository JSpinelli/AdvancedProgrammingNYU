using UnityEngine;
public static class Services {
	
	public static void InitializeServices(GameManager reference) {
		Services.GameManager = reference;
        Services.player1 = new PlayerControlled(reference.player1, 10f, reference.ball);
        Services.player2 = new ForcePlayer(reference.player2, 0.01f, reference.ball);
	}

	public static GameManager GameManager;
	public static Player player1;
	public static Player player2;
}