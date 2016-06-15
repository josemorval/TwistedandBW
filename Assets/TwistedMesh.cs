using UnityEngine;
using System.Collections;

public class TwistedMesh : MonoBehaviour {

	public Material mat;

	public int numberVerticesX;
	public int numberVerticesY;

	Vector3[] vs;
	int[] inds;
	Vector2[] uvs;

	Mesh mesh;

	void Start () {

		vs = new Vector3[numberVerticesX*numberVerticesY];
		uvs = new Vector2[numberVerticesX*numberVerticesY];

		for(int j=0;j<numberVerticesY;j++){
			for(int i=0;i<numberVerticesX;i++){

				float u = 1f*i/numberVerticesX;
				float v = 1f*j/numberVerticesY;

				vs[i+j*numberVerticesX] = new Vector3(
					(1.0f+0.8f*Mathf.Pow(Mathf.Sin(1f*(20f*v-10f)),4f))*Mathf.Cos(2f*Mathf.PI*u),
					(20f*v-10f)+1f*Mathf.Cos(2f*Mathf.PI*u),
					(1.0f+0.8f*Mathf.Pow(Mathf.Sin(1f*(20f*v-10f)),4f))*Mathf.Sin(2f*Mathf.PI*u)
				);

				uvs[i+j*numberVerticesX] = new Vector3(u,v);
			}
		}

		inds = new int[6*numberVerticesX*(numberVerticesY-1)];
		int k=0;
		for(int i=0;i<numberVerticesX*(numberVerticesY-1);i++){

			if(i%numberVerticesX==numberVerticesX-1){
				inds[0+k] = i;
				inds[1+k] = i+numberVerticesX;
				inds[2+k] = i+1-numberVerticesX;
				inds[3+k] = i+1-numberVerticesX;
				inds[4+k] = i+numberVerticesX;
				inds[5+k] = i+1;
			}else{
				inds[0+k] = i;
				inds[1+k] = i+numberVerticesX;
				inds[2+k] = i+1;
				inds[3+k] = i+numberVerticesX;
				inds[4+k] = i+numberVerticesX+1;
				inds[5+k] = i+1;
			}

			k=k+6;

		}

		mesh = new Mesh();
		mesh.vertices = vs;
		mesh.uv = uvs;
		mesh.SetIndices(inds,MeshTopology.Triangles,0);
		mesh.RecalculateNormals();

		GetComponent<MeshFilter>().mesh = mesh;

	}

	void Update(){
		transform.Rotate(new Vector3(0f,40f,0f)*Time.deltaTime);
	}

}
