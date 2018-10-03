class Lighthouse extends THREE.Group {
    constructor() {
        super();

        this.init();
    }

    init() {

        var selfRef = this;

        var mtlLoader = new THREE.MTLLoader();
        mtlLoader.setPath('models/Lighthouse/');
        var url = "lighthouse.mtl";
        mtlLoader.load(url, function (materials) {

            materials.preload();

            var objLoader = new THREE.OBJLoader();
            objLoader.setMaterials(materials);
            objLoader.setPath('models/Lighthouse/');
            objLoader.load('lighthouse.obj', function (object) {
                var group = new THREE.Group();
                object.scale.set(3, 3, 3);
                object.rotation.y = Math.PI / 2;
                object.position.set(27.5, 4.2, -6.75);
                object.rotation.y = -Math.PI / 1.125;

                group.add(object);

                var light = new THREE.SpotLight(0xffe9a2, 1, 40);
                light.position.set(27.85, 9.5, -6.9);
                light.angle = 0.25;
                group.add(light);

                var geometry = new THREE.SphereGeometry(2.5, 2.5, 2.5);
                var material = new THREE.MeshBasicMaterial({ color: 0xffe9a2 });
                var placeholder = new THREE.Mesh(geometry, material);
                placeholder.position.set(30, 2, -30);
                placeholder.rotation.y += Math.PI / 16;
                placeholder.name = "LightPointer";
                placeholder.visible = false;
                group.add(placeholder);

                // add spot light
                var geometry = new THREE.CylinderGeometry(0.1, 1.5, 10, 32 * 2, 20, true);
                geometry.applyMatrix(new THREE.Matrix4().makeTranslation(0, -geometry.parameters.height / 2, 0));
                geometry.applyMatrix(new THREE.Matrix4().makeRotationX(-Math.PI / 2));
                var material = new THREEx.VolumetricSpotLightMaterial();
                var mesh = new THREE.Mesh(geometry, material);
                mesh.position.set(27.85, 7.25, -6.9);
                material.uniforms.lightColor.value.set(0xffe9a2);
                material.uniforms.spotPosition.value = mesh.position;
                group.add(mesh);

                selfRef.add(group);
            });

        });
    }

}
