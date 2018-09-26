class Ship extends THREE.Group {
    constructor() {
        super();

        this.init();
    }

    init() {

        var selfRef = this;

        var mtlLoader = new THREE.MTLLoader();
        mtlLoader.setPath('models/Ship/');
        var url = "materials.mtl";
        mtlLoader.load(url, function (materials) {

            materials.preload();

            var objLoader = new THREE.OBJLoader();
            objLoader.setMaterials(materials);
            objLoader.setPath('models/Ship/');
            objLoader.load('model.obj', function (object) {
                var group = new THREE.Group();
                object.scale.set(10, 10, 10);
                object.rotation.y = Math.PI / 2;
                object.position.y = 1;
                object.position.z = -2;
                group.add(object);

                var light = new THREE.PointLight(0xff0000, 1, 5);
                light.position.set(-2.455, 2.90, -1.955);
                group.add(light);

                var geometry = new THREE.SphereGeometry(0.025, 0.025, 0.025);
                var material = new THREE.MeshBasicMaterial({ color: 0xffb2b2 });
                var placeholder = new THREE.Mesh(geometry, material);
                placeholder.position.set(-2.455, 2.85, -1.955);

                group.add(placeholder);

                selfRef.add(group);
            });

        });
    }

}
