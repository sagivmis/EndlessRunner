using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tester))]
public class TesterEditor : Editor
{

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        GUILayout.Space(5);
        GUILayout.Label("Player Tester");
        GUILayout.Space(20);
        GameObject player = GameObject.FindWithTag("Player");
        Cainos.CharacterController controller = (player.GetComponent<Cainos.CharacterController>());
        Animator animator = player.GetComponentInChildren<Animator>();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Revive"))
        {
            controller.Revive();
        }
        if (GUILayout.Button("Die"))
        {
            controller.Die();
        }

        GUILayout.EndHorizontal();
        if (GUILayout.Button("Pound Ground")) {
            animator.SetBool("IsAttacking",true);
            animator.SetTrigger("Pound");
            animator.SetBool("IsAttacking", false) ;
        }
        if (GUILayout.Button("Attack")) { }
        if (GUILayout.Button("Jump")) { }
    }
}