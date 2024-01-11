using System.Collections;
using UnityEngine;

public class SizeChanger : MonoBehaviour
{
    [SerializeField] private float decreaseFactor = 0.5f; // Factor by which the size will decrease
    [SerializeField] private float changeInterval = 2f; // Interval between size changes

    private Vector3 originalScale;

    private void Start()
    {
        // Save the original scale for later use
        originalScale = transform.localScale;

        // Start the continuous size change loop
        StartCoroutine(ContinuousSizeChange());
    }

    private IEnumerator ContinuousSizeChange()
    {
        while (true)
        {
            // Decrease the size
            Vector3 decreasedSize = originalScale * decreaseFactor;
            yield return StartCoroutine(SizeChangeAnimation(transform.localScale, decreasedSize, 1f)); // Adjust the duration

            // Wait for a moment before increasing the size
            yield return new WaitForSeconds(changeInterval);

            // Increase the size back to the original
            yield return StartCoroutine(SizeChangeAnimation(transform.localScale, originalScale, 1f)); // Adjust the duration

            // Wait for a moment before starting the loop again
            yield return new WaitForSeconds(changeInterval);
        }
    }

    private IEnumerator SizeChangeAnimation(Vector3 startSize, Vector3 targetSize, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startSize, targetSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the final size is exactly the target size
        transform.localScale = targetSize;
    }
}
