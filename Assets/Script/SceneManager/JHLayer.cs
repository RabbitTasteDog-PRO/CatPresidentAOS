using UnityEngine;
using System.Collections;

/// <summary>
/// BCScene 안에 들어가는 레이어, 해당 클레스는 상속받아서 사용한다.
/// </summary>
public class JHLayer : MonoBehaviour {
	///back키 사용여부
	public bool backKeyEnable = true;
	public UIButton btnClose;

	///레이어의 그룹 아이디
	public int groupId = 0;

	///부모 Scene, startLayer 전에 Scene에서 설정 해 준다.
	private JHScene _parentScene;
	public JHScene parentScene {
		get {
			return _parentScene;
		}
		set {
			_parentScene = value;
		}
	}
	
	///레이어 시작 시 호출된다. override 필수
	public virtual void StartLayer() {
	}

	///레이어 종료 시 호출된다. override 필수
	public virtual void EndLayer() {
		if (parentScene != null) {
			parentScene.RemoveLayer(this);
			parentScene = null;
		}
	}


}
