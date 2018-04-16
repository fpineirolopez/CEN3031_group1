using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class RandomScalingFunctionTest {

	[Test]
	public void enemy_count_range_test() {
        RandomProgression random_prog_functions = new RandomProgression();
        for( int x  = 1; x <= 20; x++)
        {
            for( int y = 0; y < 100; y++)
            {
                int enemy_count = random_prog_functions.floor_number_to_enemy_count(x);
                Assert.GreaterOrEqual( enemy_count, 1  );
                Assert.LessOrEqual( enemy_count, 20);

                if (x < 11)
                {
                    Assert.GreaterOrEqual(enemy_count, Mathf.Ceil((x + 2) *(1.0f - x*.05f) ) );
                    Assert.LessOrEqual(enemy_count, Mathf.Ceil((x + 2) * ( 1.0f + x*.05f ))  ) ;

                }

                else
                {
                    Assert.GreaterOrEqual(enemy_count, Mathf.Ceil(13 * 0.5f));
                    Assert.LessOrEqual(enemy_count, Mathf.Ceil(13 * 1.5f));
                }

            }
        }
	}

    [Test]
    public void room_count_range_test()
    {
        RandomProgression random_prog_functions = new RandomProgression();
        for (int x = 1; x <= 20; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                int room_count = random_prog_functions.floor_number_to_room_count(x);
                Assert.GreaterOrEqual(room_count, 1);
                Assert.LessOrEqual(room_count, 20);

                if (x < 11)
                {
                    Assert.GreaterOrEqual(room_count, Mathf.Ceil((x + 3) * (1.0f - x * .025f)));
                    Assert.LessOrEqual(room_count, Mathf.Ceil((x + 3) * (1.0f + x * .025f)));

                }

                else
                {
                    Assert.GreaterOrEqual(room_count, Mathf.Ceil(14 * 0.75f));
                    Assert.LessOrEqual(room_count, Mathf.Ceil(14 * 1.25f));
                }

            }
        }
    }

}
