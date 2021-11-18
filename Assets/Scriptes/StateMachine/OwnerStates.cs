using Lofle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ExampleOwner : MonoBehaviour
{
	private class FirstOwnerState : State<ExampleOwner>
	{
		private int _count = 0;

		protected override void Begin()
		{
			Debug.Log( "Begin FirstOwnerState" );
			Owner.Print( "FirstOwnerState!" );
			_count = _DEFAULT_COUNT;
		}

		protected override void Update()
		{
			if( _count-- <= 0 )
			{
				Invoke<SecondOwnerState>();
			}
		}

		protected override void End()
		{
			Debug.Log( "End FirstOwnerState" );
		}
	}

	private class SecondOwnerState : State<ExampleOwner>
	{
		private int _count = 0;

		protected override void Begin()
		{
			Debug.Log( "Begin SecondOwnerState" );
			Owner.Print( "SecondOwnerState!" );
			_count = _DEFAULT_COUNT;
		}

		protected override void Update()
		{
			if( _count-- <= 0 )
			{
				Invoke<FirstOwnerState>();
			}
		}

		protected override void End()
		{
			Debug.Log( "End SecondOwnerState" );
		}
	}
}