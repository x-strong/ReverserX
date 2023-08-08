namespace Spice86.Core.Emulator.CPU.CfgCpu.ParsedInstruction;

using System.Collections.Generic;

/// <summary>
/// Represents the value of the discriminator of one instruction
/// </summary>
public class InstructionDiscriminator : IDiscriminated {
    public InstructionDiscriminator(IList<byte?> discriminatorValue) {
        DiscriminatorValue = discriminatorValue;
    }

    /// <summary>
    /// Value of the discriminator
    /// </summary>
    public IList<byte?> DiscriminatorValue { get; }

    public bool Equals(Span<byte> bytes) {
        if (DiscriminatorValue.Count != bytes.Length) {
            return false;
        }

        for (int i = 0; i < DiscriminatorValue.Count; i++) {
            if (Differs(i, bytes[i])) {
                return false;
            }
        }

        return true;
    }
    
    public bool Equals(IList<byte> bytes) {
        if (DiscriminatorValue.Count != bytes.Count) {
            return false;
        }

        for (int i = 0; i < DiscriminatorValue.Count; i++) {
            if (Differs(i, bytes[i])) {
                return false;
            }
        }

        return true;
    }

    private bool Differs(int i, byte b) {
        byte? d = DiscriminatorValue[i];
        if (d is null) {
            return false;
        }

        if (d != b) {
            return true;
        }

        return false;
    }
    
    protected bool Equals(InstructionDiscriminator other) {
        return DiscriminatorValue.Equals(other.DiscriminatorValue);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) {
        if (ReferenceEquals(null, obj)) {
            return false;
        }

        if (ReferenceEquals(this, obj)) {
            return true;
        }

        if (obj.GetType() != this.GetType()) {
            return false;
        }

        return Equals((InstructionDiscriminator)obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode() {
        return DiscriminatorValue.GetHashCode();
    }
}