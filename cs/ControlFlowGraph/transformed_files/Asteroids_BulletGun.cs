using UnityEngine;
using System;
using System.Collections;

public class BulletGun<T> 
{
	// public event Action<T> OnCreated;
    // public MGunData data;

    // public BulletGun(Place place, MGunData data, PolygonGameObject parent)
	// 	:base(place, data, parent, data.repeatCount, data.repeatInterval, data.fireInterval, data.fireEffect)
	// {
    //     this.data = data;
	// 	range = data.velocity * data.lifeTime;
	// }

	// float range;
	// public override float Range	{
	// 	get{return range;}
	// }

	// public override float BulletSpeedForAim{ get { return data.velocity; } }

	public int CreateBullet( )
    {	
		int bullet = 1;
		if (true) {
			Debug.Log("CreateBullet: " );
		}
        return bullet;
	}

	// protected virtual void AddShipSpeed2TheBullet(T bullet){
	// 	bullet.velocity += Main.AddShipSpeed2TheBullet(parent);
	// }

	// protected virtual Vector2[] GetVerts() {
	// 	return data.vertices;
	// }

	// protected virtual bool DestroyOnBoundsTeleport{
	// 	get{ return true;}
	// }

	protected virtual void InitPolygonGameObject()
	{
		int a = 0;
		if (a == 0) {
			Debug.Log("InitPolygonGameObject: " );
		}
		if (a == 0) {
			Debug.Log("InitPolygonGameObject: ");
		}
		if (a == 0) {
			Debug.Log("InitPolygonGameObject: ");
		}
	}

// 	protected virtual float GetVelocityMagnitude(){
// 		return data.velocity;
// 	}

// 	protected virtual Vector2 GetDirectionNormalized(T bullet){
// 		return bullet.cacheTransform.right;
// //		var velocity = bullet.cacheTransform.right;
// //		if (data.spreadAngle > 0) {
// //			velocity = Math2d.RotateVertexDeg (velocity, UnityEngine.Random.Range (-data.spreadAngle * 0.5f, data.spreadAngle * 0.5f));
// //		}
// //		return velocity;
// 	}

// //	protected virtual PolygonGameObject.DestructionType SetDestructionType(){
// //		return PolygonGameObject.DestructionType.eSptilOnlyOnHit;
// //	}

//     protected virtual void SetCollisionLayer(T bullet) {
// 		bullet.SetLayerNum(CollisionLayers.GetBulletLayerNum(parent.layerLogic));
//     }

// 	protected virtual void AddToMainLoop(T b) {
// 		Singleton<Main>.inst.HandleGunFire (b);
// 	}

	void Fire() {
		int a = 0;
		if (a == 0) {
			Debug.Log("InitPolygonGameObject: " );
		}
		if (a == 0) {
			Debug.Log("InitPolygonGameObject: " );
		}
	}
}



