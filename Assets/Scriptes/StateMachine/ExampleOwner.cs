using Lofle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ExampleOwner : MonoBehaviour
{
	private const int _DEFAULT_COUNT = 120;

	private StateMachine _stateMachine = null;
	private StateMachine<ExampleOwner> _ownerStateMachine = null;

	private void Start()
	{
		_stateMachine = new StateMachine();
		_ownerStateMachine = new StateMachine<ExampleOwner>( this );

		StartCoroutine( _stateMachine.Coroutine<FirstState>() );
		StartCoroutine( _ownerStateMachine.Coroutine<FirstOwnerState>() );
	}

	private void OnGUI()
	{
		GUILayout.Label( string.Format( "_stateMachine Current State : {0}", _stateMachine.CurrentState ) );
		GUILayout.Label( string.Format( "_ownerStateMachine Current State : {0}", _ownerStateMachine.CurrentState ) );
	}

	public void Print( string text )
	{
		Debug.Log( text );
	}
}
