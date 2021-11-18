using Lofle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ExampleOwner : MonoBehaviour
{
	private class FirstState : State
	{
		private int _count = 0;

		protected override void Begin()
		{
			Debug.Log( "Begin FirstState" );
			_count = _DEFAULT_COUNT;
		}

		protected override void Update()
		{
			if( _count-- <= 0 )
			{
				Invoke<SecondState>();
			}
		}

		protected override void End()
		{
			Debug.Log( "End FirstState" );
		}
	}

	private class SecondState : State
	{
		private int _count = 0;

		protected override void Begin()
		{
			_count = _DEFAULT_COUNT;
			Debug.Log( "Begin SecondState" );
		}

		protected override void Update()
		{
			if( _count-- <= 0 )
			{
				Invoke<FirstState>();
			}
		}

		protected override void End()
		{
			Debug.Log( "End SecondState" );
		}
	}
}
